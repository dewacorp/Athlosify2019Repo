using AthlosifyMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net;
using Xamarin.Essentials;
using AthlosifyMobileApp.Helpers;

namespace AthlosifyMobileApp.Services
{
    public class AccountApiService 
    {
		public async Task<bool> RegisterUser(string FirstName, string LastName, float? Handicap, string UserRole, string email, string password, string confirmPassword)
		{
			var registerModel = new RegisterModel()
			{
				FirstName = FirstName,
				LastName = LastName,
				Handicap = Handicap,
				UserRole = UserRole,
				Email = email,
				Password = password,
				ConfirmPassword = confirmPassword
			};

			var httpClient = new HttpClient();

			var json = JsonConvert.SerializeObject(registerModel);

			var content = new StringContent(json, Encoding.UTF8, Constant.MediaType_JSON);

			var response = await httpClient.PostAsync(Constant.Route_Account_Register, content);

			return response.IsSuccessStatusCode;

		}

		public async Task<TokenResponse> GetToken(string email, string password)
		{
			var httpClient = new HttpClient();

			var content = new StringContent($"grant_type=password&username={email}&password={password}", Encoding.UTF8, 
									Constant.MediaType_FormURLEncoded);

			var response = await httpClient.PostAsync(Constant.Route_Token, content);

			var jsonResult = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<TokenResponse>(jsonResult);

			return result;
		}

		public async Task<bool> PasswordRecovery(string email)
		{
			var httpClient = new HttpClient();

			var recoveryPasswordModel = new PasswordRecoveryModel()
			{
				Email = email
			};

			var json = JsonConvert.SerializeObject(recoveryPasswordModel);

			var content = new StringContent(json, Encoding.UTF8, Constant.MediaType_JSON);

			var response = await httpClient.PostAsync(Constant.Route_Users_PasswordRecovery, content);

			return response.IsSuccessStatusCode;
		}

		public async Task<bool> ChangePassword(string oldPassword, string newPassword, string confirmPassword)
		{
			var httpClient = new HttpClient();

			var changePasswordModel = new ChangePasswordModel()
			{
				OldPassword = oldPassword,
				NewPassword = newPassword,
				ConfirmPassword = confirmPassword
			};

			var json = JsonConvert.SerializeObject(changePasswordModel);

			var content = new StringContent(json, Encoding.UTF8, Constant.MediaType_JSON);

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get(Constant.Setting_AccessToken, ""));

			var response = await httpClient.PostAsync(Constant.Route_Account_ChangePassword, content);

			return response.IsSuccessStatusCode;
		}

		

	}
}
