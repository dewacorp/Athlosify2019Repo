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
    public class ForgotPasswordViewModel : BaseViewModel
    {
		private readonly AccountApiService _accountService = new AccountApiService();

		public ICommand SendCommand { get; private set; }

		private string _email;

		public string Email
		{
			get => _email;
			set => SetProperty(ref _email, value);
		}

		public ForgotPasswordViewModel()
		{
			SendCommand = new Command(async () => await SendAccount());
		}

		public async Task SendAccount()
		{
			bool response = await _accountService.PasswordRecovery(Email);
			if (!response)
			{
				Application.Current.MainPage.DisplayAlert("Account", "Error.", "Ok");
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("Account", "A new password has been sent to your email.", "Ok");
				Navigation.PopToRootAsync();
			}
		}
	}
}
