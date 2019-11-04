using AthlosifyMobileApp.Helpers;
using AthlosifyMobileApp.Interfaces;
using AthlosifyMobileApp.Models;
using AthlosifyMobileApp.Services;
using AthlosifyMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AthlosifyMobileApp.ViewModels
{
	public class ActivityDetailViewModel : BaseViewModel
	{
		private readonly ActivityApiService _activityService = new ActivityApiService();

		public Activity Activity { get; set; }

		private ActivityValidator _activityValidator;

		public ICommand DeleteCommand { get; private set; }
		public ICommand SaveCommand { get; private set; }

		List<Category> _categoryList;

		public List<Category> CategoryList
		{
			get => _categoryList;
			set => SetProperty(ref _categoryList, value);
		}

		private Category _selectedCategory;

		public Category SelectedCategory
		{
			get => _selectedCategory;
			set => SetProperty(ref _selectedCategory, value);
		}

		public ActivityDetailViewModel(Activity activity)
		{
			PageTitle = "Activity";
			_activityValidator = new ActivityValidator();

			Activity = activity ?? new Activity();

			DeleteCommand = new Command(async () => await DeleteActivity());
			SaveCommand = new Command(async () => await SaveActivity());
			LoadAsyncdata();
		}

		private async Task LoadAsyncdata()
		{
			Task.Run(async () => { await FetchCategories(); }).Wait();
			Task.Run(async () => { await FetchActivityDetail(); });
		}


		public async Task FetchActivityDetail()
		{
			if (Activity.Id > 0)
			{
				Activity = await _activityService.GetActivity(Activity.Id);
				SelectedCategory = CategoryList.FirstOrDefault(x => x.Id == Activity.CategoryId);
			}
		}

		public async Task FetchCategories()
		{
			CategoryResult categoryResult = await _activityService.GetCategories();
			CategoryList = categoryResult.Results;
		}


		private async Task SaveActivity()
		{
			Activity.CategoryId = SelectedCategory.Id;
			Activity.CategoryName = SelectedCategory.Name;

			var validationResults = _activityValidator.Validate(Activity);

			if (validationResults.IsValid)
			{
				if (Activity.Id != 0)
				{
					bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Activity", "Update the details", "OK", "Cancel");
					if (isUserAccept)
					{
						var response = await _activityService.UpdateActivity(Activity.Id, Activity);
						if (!response)
						{
							Application.Current.MainPage.DisplayAlert("Activity", "Error", "Alright");
						}
						else
						{
							Navigation.PushAsync(new ActivityListPage());
						}
					}
				}
				else
				{
					bool isUserAccept = await Application.Current.MainPage.DisplayAlert("Activity", "Add new details", "OK", "Cancel");
					if (isUserAccept)
					{
						var response = await _activityService.AddActivity(Activity);
						if (!response)
						{
							Application.Current.MainPage.DisplayAlert("Activity", "Error", "Alright");
						}
						else
						{
							Navigation.PushAsync(new ActivityListPage());
						}
					}
				}
			}
			else
			{
				Application.Current.MainPage.DisplayAlert("Activity", validationResults.Errors[0].ErrorMessage, "Ok");
			}
		}
		

		public async Task DeleteActivity()
		{
			var alert = await Application.Current.MainPage.DisplayAlert("Activity", "Do you want to delete this item?", "Yes", "Cancel");
			if (alert)
			{
				var response = await _activityService.DeleteActivity(Activity.Id);
				if (!response)
				{
					Application.Current.MainPage.DisplayAlert("Activity", "Something wrong", "Alright");
				}
				else
				{
					Navigation.PushAsync(new ActivityListPage());
				}
			}
		}

		
	}
}