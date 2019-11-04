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
	public partial class ActivityListPage : ContentPage
	{
		private ActivityListViewModel _viewModel;

		public ActivityListPage()
		{
			InitializeComponent();
			BindingContext = _viewModel = new ActivityListViewModel()
			{
				Navigation = Navigation
			};
		}
	}
}