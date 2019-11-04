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
    public class LoginViewModel : BaseViewModel
    {
		private readonly AccountApiService _accountService = new AccountApiService();

		public ICommand LoginCommand { get; private set; }

		public ICommand ForgotPasswordCommand { get; private set; }

		public ICommand SignUpCommand { get; private set; }

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

		public LoginViewModel()
		{
			LoginCommand = new Command(async () => await LoginAccount());
			ForgotPasswordCommand = new Command(async () => await ForgotPasswordAccount());
			SignUpCommand = new Command(async () => await SignUpAccount());
		}


		public async Task LoginAccount()
		{
			var response = await _accountService.GetToken(Email, Password);
			if (string.IsNullOrEmpty(response.access_token))
			{
				Application.Current.MainPage.DisplayAlert("Account", "Error.", "Ok");
			}
			else
			{
				Preferences.Set(Constant.Setting_UserEmail, Email);
				Preferences.Set(Constant.Setting_Password, Password);
				Preferences.Set(Constant.Setting_AccessToken, response.access_token);
				Preferences.Set(Constant.Setting_UserId, response.userID);
				Application.Current.MainPage = new MasterPage();
			}
		}

		public async Task ForgotPasswordAccount()
		{
			Navigation.PushAsync(new ForgotPasswordPage());
		}

		public async Task SignUpAccount()
		{
			Navigation.PushAsync(new SignUpPage());
		}

	}
}
