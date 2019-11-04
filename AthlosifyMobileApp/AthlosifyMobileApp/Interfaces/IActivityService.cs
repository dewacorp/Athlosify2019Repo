using AthlosifyMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AthlosifyMobileApp.Interfaces
{
    public interface IActivityService
    {
		Task<Activity> GetActivity(int activityId);

		Task<bool> AddActivity(Activity activity);

		Task<CategoryResult> GetCategories();

		Task<bool> UpdateActivity(int id, Activity activity);

		Task<bool> DeleteActivity(int id);

		Task<ActivityResult> GetActivities(string sortFields, int pageNumber, int pageSize);

	}
}
