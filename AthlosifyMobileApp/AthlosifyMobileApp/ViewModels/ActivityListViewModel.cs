using AthlosifyMobileApp.Models;
using AthlosifyMobileApp.Views;
using AthlosifyMobileApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using AthlosifyMobileApp.Helpers;
using AthlosifyMobileApp.Interfaces;

namespace AthlosifyMobileApp.ViewModels
{
	//public class ActivityListViewModel : ActivityBaseViewModel
	public class ActivityListViewModel : BaseViewModel

	{
		
		private const int PageSize = 10;
		private int totalRecords;

		private readonly ActivityApiService _activityService = new ActivityApiService();

		

		private InfiniteScrollCollection<Activity> _activityList;

		public InfiniteScrollCollection<Activity> ActivityList
		{
			get => _activityList;
			set => SetProperty(ref _activityList, value); 
		}

		public ICommand SearchCommand { private set; get; }

		public ICommand AddCommand { private set; get;	}

		private Activity _selectedActivity;

		public Activity SelectedItem
		{
			get => _selectedActivity;
			set
			{
				SetProperty(ref _selectedActivity, value);
				if (_selectedActivity != null)
					ShowActivityDetails(_selectedActivity);

			}
		}

		async void ShowActivityDetails(Activity selectedActivity)
		{
			await Navigation.PushAsync(new ActivityDetailPage(selectedActivity));
		}

		public ActivityListViewModel()
		{
			PageTitle = "Activity";

			ActivityList = new InfiniteScrollCollection<Activity>
			{		
				OnLoadMore = async () =>
				{
					IsBusy = true;
					int page = ActivityList.Count / PageSize;
					ActivityResult activityResult = await _activityService.GetActivities("-CreatedDate", page+1, PageSize);
					IsBusy = false;
					return activityResult.Results;
				},
				OnCanLoadMore = () =>
				{
					return ActivityList.Count < totalRecords;
				}
			};
			DownloadDataAsync();

			SearchCommand = new Command(async () => await GotoSearchPage());
			AddCommand = new Command(async () => await GotoAddPage());	
		}

		public async Task GotoSearchPage()
		{
			await Navigation.PushAsync(new ActivitySearchPage());
		}

		public async Task GotoAddPage()
		{
			await Navigation.PushAsync(new ActivityDetailPage());
		}

		private async  Task DownloadDataAsync()
		{
			ActivityResult items = await _activityService.GetActivities("-CreatedDate", 1, PageSize);
			totalRecords = items.Metadata.TotalRecords;
			ActivityList.AddRange(items.Results);
		}


	}
}
