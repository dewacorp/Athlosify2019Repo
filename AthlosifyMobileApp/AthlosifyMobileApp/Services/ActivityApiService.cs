using AthlosifyMobileApp.Helpers;
using AthlosifyMobileApp.Interfaces;
using AthlosifyMobileApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AthlosifyMobileApp.Services
{
    public class ActivityApiService 
    {
		public async Task<bool> AddActivity(Activity activity)
		{
			var httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(activity);

			var content = new StringContent(json, Encoding.UTF8, Constant.MediaType_JSON);

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.PostAsync(Constant.Route_Activities, content);

			return response.IsSuccessStatusCode;

		}

		public async Task<bool> UpdateActivity(int id, Activity activity)
		{
			var httpClient = new HttpClient();
			var json = JsonConvert.SerializeObject(activity);

			var content = new StringContent(json, Encoding.UTF8, Constant.MediaType_JSON);

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.PutAsync(Constant.Route_Activities + "/" + id, content);

			return response.IsSuccessStatusCode;

		}

		public async Task<bool> DeleteActivity(int id)
		{
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.DeleteAsync(Constant.Route_Activities + "/" + id);

			return response.IsSuccessStatusCode;

		}

		public async Task<ActivityResult> GetActivities(string sortFields, int pageNumber, int pageSize)
		{
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.GetStringAsync(Constant.Route_Activities + "?sortfields=" + sortFields + "&pagenumber=" + pageNumber + "&pagesize=" + pageSize);

			return JsonConvert.DeserializeObject<ActivityResult>(response);
		}

		public async Task<Activity> GetActivity(int id)
		{
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.GetStringAsync(Constant.Route_Activities + "/" + id);

			return JsonConvert.DeserializeObject<Activity>(response);
		}

		public async Task<ActivityResult> SearchActivities(string keyword, string sortFields, int pageNumber, int pageSize)
		{
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.GetStringAsync(Constant.Route_Activities + "/" + keyword +
									"/" + sortFields + "/" + pageNumber + "/" + pageSize);

			return JsonConvert.DeserializeObject<ActivityResult>(response);
		}

		public async Task<CategoryResult> GetCategories()
		{
			var httpClient = new HttpClient();

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.GetStringAsync(Constant.Route_Categories);

			return JsonConvert.DeserializeObject<CategoryResult>(response);
		}
	}
}
