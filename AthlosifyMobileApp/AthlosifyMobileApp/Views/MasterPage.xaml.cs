using AthlosifyMobileApp.Views;
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
    public partial class MasterPage : MasterDetailPage
    {
		private MasterViewModel _viewModel;

		public MasterPage()
        {
            InitializeComponent();
			BindingContext = _viewModel = new MasterViewModel()
			{
				Navigation = Navigation
			};
		}

	}
}