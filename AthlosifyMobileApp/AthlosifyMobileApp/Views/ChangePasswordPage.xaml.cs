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
	public partial class ChangePasswordPage : ContentPage
	{
		private ChangePasswordViewModel _viewModel;

		public ChangePasswordPage ()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ChangePasswordViewModel()
			{
				Navigation = Navigation
			};
		}
		
	}
}