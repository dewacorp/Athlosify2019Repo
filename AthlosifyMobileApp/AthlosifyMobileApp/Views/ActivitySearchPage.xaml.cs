using AthlosifyMobileApp.ViewModels;
using AthlosifyMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AthlosifyMobileApp.Services;

namespace AthlosifyMobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ActivitySearchPage : ContentPage
	{
		private ActivitySearchViewModel _viewModel;

		public ActivitySearchPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ActivitySearchViewModel()
			{
				Navigation = Navigation
			};
		}
	}
}