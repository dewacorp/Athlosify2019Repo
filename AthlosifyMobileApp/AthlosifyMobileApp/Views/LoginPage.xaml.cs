using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AthlosifyMobileApp.ViewModels;

namespace AthlosifyMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		private LoginViewModel _viewModel;

		public LoginPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new LoginViewModel()
			{
				Navigation = Navigation
			};
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}