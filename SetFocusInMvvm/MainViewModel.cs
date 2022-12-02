using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SetFocusInMvvm;
public class MainViewModel : INotifyPropertyChanged
{
	private string? _userName;

	private string? _password;

	private string? _message;

	private string? _focusTargetPropertyName;

	private RelayCommand? _loginCommand;

	public string? UserName
	{
		get => _userName;
		set
		{
			_userName = value;
			OnPropertyChanged();
		}
	}

	public string? Password
	{
		get => _password;
		set
		{
			_password = value;
			OnPropertyChanged();
		}
	}

	public string? Message
	{
		get => _message;
		set
		{
			_message = value;
			OnPropertyChanged();
		}
	}

	public string? FocusTargetPropertyName
	{
		get => _focusTargetPropertyName;
		set
		{
			_focusTargetPropertyName = value;
			OnPropertyChanged();
		}
	}

	public ICommand LoginCommand => _loginCommand ??= new(Login);

	public event PropertyChangedEventHandler? PropertyChanged;

	private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new(propertyName));
	}

	private void Login()
	{
		if (string.IsNullOrEmpty(UserName))
		{
			Message = "사용자 이름을 입력하세요";
			FocusTargetPropertyName = nameof(UserName);
			return;
		}
		else if (string.IsNullOrEmpty(Password))
		{
			Message = "패스워드를 입력하세요.";
			FocusTargetPropertyName = nameof(Password);
			return;
		}

		Message = "로그인 성공!";
	}
}
