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
    public class ChangePasswordViewModel : BaseViewModel
    {
		private readonly AccountApiService _accountService = new AccountApiService();

		public ICommand ChangePasswordCommand { get; private set; }

		private string _oldPassword;

		public string OldPassword
		{
			get => _oldPassword;
			set => SetProperty(ref _oldPassword, value);
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

		public ChangePasswordViewModel()
		{
			ChangePasswordCommand = new Command(async () => await ChangePasswordAccount());
		}

		public async Task ChangePasswordAccount()
		{
			var response = await _accountService.ChangePassword(OldPassword, Password, ConfirmPassword);
			if (!response)
			{
				Application.Current.MainPage.DisplayAlert("Account", "Error.", "Ok");
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("Account", "Password has been changed.", "Ok");
				Application.Current.MainPage = new NavigationPage(new LoginPage());
			}
		}
	}
}
