using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup.Primitives;

namespace SetFocusInMvvm;
public static class DependencyObjectExtension
{
	public static DependencyObject? FindBindingTarget(this DependencyObject d, string propertyName)
	{
		ArgumentNullException.ThrowIfNull(d);

		var bindings = d.GetBindableProperties().Select(dp => BindingOperations.GetBindingBase(d, dp)).Where(b => b is not null);

		bool BindingPathIsTargetProperty(BindingBase bindingBase)
		{
			if (bindingBase is Binding b)
			{
				return b.Path.Path == propertyName;
			}
			else if (bindingBase is MultiBinding mb)
			{
				return mb.Bindings.Any(BindingPathIsTargetProperty);
			}
			else if (bindingBase is PriorityBinding pb)
			{
				return pb.Bindings.Any(BindingPathIsTargetProperty);
			}

			return false;
		}

		if (bindings.Any(BindingPathIsTargetProperty))
		{
			return d;
		}

		foreach(var child in LogicalTreeHelper.GetChildren(d).OfType<DependencyObject>())
		{
			var ret = child.FindBindingTarget(propertyName);
			if (ret is not null)
			{
				return ret;
			}
		}

		return null;
	}

	private static IEnumerable<DependencyProperty> GetBindableProperties(this DependencyObject d)
	{
		return MarkupWriter.GetMarkupObjectFor(d).Properties.Where(p => p.DependencyProperty is not null).Select(p => p.DependencyProperty)
			.Union(MarkupWriter.GetMarkupObjectFor(d).Properties.Where(p => p.DependencyProperty is not null && p.IsAttached).Select(p => p.DependencyProperty));
	}
}