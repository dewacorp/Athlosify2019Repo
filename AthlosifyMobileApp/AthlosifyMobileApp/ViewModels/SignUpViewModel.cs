using AthlosifyMobileApp.Helpers;
using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AthlosifyMobileApp.ViewModels
{
    public class SignUpViewModel : BaseViewModel
    {
		private readonly AccountApiService _accountService = new AccountApiService();

		public ICommand SignUpCommand { get; private set; }

		private string _firstName;

		public string FirstName
		{
			get => _firstName;
			set => SetProperty(ref _firstName, value);
		}

		private string _lastName;

		public string LastName
		{
			get => _lastName;
			set => SetProperty(ref _lastName, value);
		}

		private float? _handicap;

		public float? Handicap
		{
			get => _handicap;
			set => SetProperty(ref _handicap, value);
		}

		private string _userRole;

		public string UserRole
		{
			get => _userRole;
			set => SetProperty(ref _userRole, value);
		}

		private string _email;

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		private string _password;

		public string Password
		{
			get => _password;
			set => SetProperty(ref _password, value);
		}

		private string _confirmPassword;

		public string ConfirmPassword
		{
			get => _confirmPassword;
			set => SetProperty(ref _confirmPassword, value);
		}

		private string _selectedUserRole;

		public string SelectedUserRole
		{
			get => _selectedUserRole;
			set => SetProperty(ref _selectedUserRole, value);
		}

		public SignUpViewModel()
		{
			SignUpCommand = new Command(async () => await SignUpAccount());
		}

		public async Task SignUpAccount()
		{
			var response = await _accountService.RegisterUser(FirstName, LastName, Handicap, SelectedUserRole, Email, Password, ConfirmPassword);
			if (!response)
			{
				Application.Current.MainPage.DisplayAlert("Account", "Error or the account exists already.", "Ok");
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("Account", "Your account has been created.", "Ok");
				Navigation.PopToRootAsync();
			}
		}
	}
}
