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
	public class ActivitySearchViewModel : BaseViewModel

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

		private string _keyword;

		public string Keyword
		{
			get => _keyword;
			set => SetProperty(ref _keyword, value);
		}

		async void ShowActivityDetails(Activity selectedActivity)
		{
			await Navigation.PushAsync(new ActivityDetailPage(selectedActivity));
		}

		public ActivitySearchViewModel()
		{
			PageTitle = "Activity";

			SearchCommand = new Command(async () => await SearchActivity());
		}

		public async Task SearchActivity()
		{
			ActivityList = new InfiniteScrollCollection<Activity>
			{
				OnLoadMore = async () =>
				{
					IsBusy = true;
					int page = ActivityList.Count / PageSize;
					ActivityResult activityResult = await _activityService.SearchActivities(_keyword, "-CreatedDate", page + 1, PageSize);
					IsBusy = false;
					return activityResult.Results;
				},
				OnCanLoadMore = () =>
				{
					return ActivityList.Count < totalRecords;
				}
			};
			DownloadDataAsync(_keyword);
		}

		private async  Task DownloadDataAsync(string keyword)
		{
			ActivityResult items = await _activityService.SearchActivities(keyword, "-CreatedDate", 1, PageSize);
			totalRecords = items.Metadata.TotalRecords;
			ActivityList.AddRange(items.Results);
		}


	}
}
