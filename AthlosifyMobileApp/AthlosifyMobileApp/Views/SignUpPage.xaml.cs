using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AthlosifyMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SignUpPage : ContentPage
	{
		private SignUpViewModel _viewModel;

		public SignUpPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new SignUpViewModel()
			{
				Navigation = Navigation
			};
		}
	}
}