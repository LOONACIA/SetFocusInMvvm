using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SetFocusInMvvm;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();
	}

	private void FocusTargetPropertyChanged(object sender, TextChangedEventArgs e)
	{
		if (string.IsNullOrEmpty(xFocusHandler.Text))
		{
			e.Handled = true;
			return;
		}

		string propertyName = xFocusHandler.Text;
		var obj = this.FindBindingTarget(propertyName);
		if (obj is null)
		{
			return;
		}

		if (obj is FrameworkElement fe)
		{
			fe.Focus();
		}
		xFocusHandler.SetCurrentValue(TextBox.TextProperty, string.Empty);
	}
}
