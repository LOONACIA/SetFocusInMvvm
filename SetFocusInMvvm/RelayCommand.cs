using System;
using System.Windows.Input;

namespace SetFocusInMvvm;

internal class RelayCommand : ICommand
{
	private readonly Action _execute;

	public event EventHandler? CanExecuteChanged;

	public RelayCommand(Action execute)
	{
		ArgumentNullException.ThrowIfNull(execute);

		_execute = execute;
	}

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public void Execute(object? parameter) => _execute();
}