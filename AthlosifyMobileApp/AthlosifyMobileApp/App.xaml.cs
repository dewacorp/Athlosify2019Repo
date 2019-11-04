using AthlosifyMobileApp.Views;
using AthlosifyMobileApp.Helpers;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Unity;
using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.Interfaces;
using CommonServiceLocator;
using Unity.ServiceLocation;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AthlosifyMobileApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			if (!string.IsNullOrEmpty(Preferences.Get(Constant.Setting_AccessToken, "")))
			{
				MainPage = new MasterPage();
			}
			else if (string.IsNullOrEmpty(Preferences.Get(Constant.Setting_UserEmail, "")) && 
				string.IsNullOrEmpty(Preferences.Get(Constant.Setting_Password, "")))
			{
				MainPage = new NavigationPage(new LoginPage());
			}
			
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
