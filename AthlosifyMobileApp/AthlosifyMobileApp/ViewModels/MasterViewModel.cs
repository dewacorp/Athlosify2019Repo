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
    public class MasterViewModel : BaseViewModel
    {
		public ICommand HomeCommand { get; private set; }

		public ICommand ActivitiesCommand { get; private set; }

		public ICommand ChangePasswordCommand { get; private set; }

		public ICommand LogoutCommand { get; private set; }

		public ICommand AboutCommand { get; private set; }

		public MasterViewModel()
		{
			HomeCommand = new Command(async () => await GoToHome());
			ActivitiesCommand = new Command(async () => await GoToActivities());
			ChangePasswordCommand = new Command(async () => await GoToChangePassword());
			LogoutCommand = new Command(async () => await Logout());
			AboutCommand = new Command(async () => await GoToAbout());
		}

		public async Task GoToHome()
		{
			((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new HomePage());
			((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
		}

		public async Task GoToActivities()
		{
			((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ActivityListPage());
			((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
		}

		public async Task GoToChangePassword()
		{
			((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new ChangePasswordPage());
			((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
		}

		public async Task Logout()
		{
			Preferences.Set(Constant.Setting_UserEmail, String.Empty);
			Preferences.Set(Constant.Setting_Password, String.Empty);
			Preferences.Set(Constant.Setting_AccessToken, String.Empty);
			Application.Current.MainPage = new NavigationPage(new LoginPage());
		}

		public async Task GoToAbout()
		{
			((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage(new AboutPage());
			((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
		}

	}
}
