using AthlosifyMobileApp.Models;
using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.ViewModels;
using CommonServiceLocator;
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
	public partial class ActivityDetailPage : ContentPage
	{
		private ActivityDetailViewModel _viewModel;

		public ActivityDetailPage(Activity activity = null)
		{
			InitializeComponent ();
			BindingContext = _viewModel = new ActivityDetailViewModel(activity)
			{
				Navigation = Navigation
			};
		}
	}
}