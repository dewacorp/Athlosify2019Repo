using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Helpers
{
	public static class Constant
	{
		// Development - Android
		public static string WebAPIURLBase						= "http://10.0.2.2:3626";

		// Development - iOS
		//public static string WebAPIURLBase					= "http://localhost:3626";

		// Production - Android and iOS
		//public static string WebAPIURLBase					= "https://athlosify.azurewebsites.net";

		public static string Route_Token							= WebAPIURLBase + "/Token";

		public static string Route_Account							= WebAPIURLBase + "/api/Account";
		public static string Route_Account_Register					= Route_Account + "/Register";
		public static string Route_Account_ChangePassword			= Route_Account + "/ChangePassword";

		public static string Route_Activities						= WebAPIURLBase + "/api/v1/Activities";

		public static string Route_Categories						= WebAPIURLBase + "/api/v1/Categories";

		public static string Route_Users							= WebAPIURLBase + "/api/v1/Users";
		public static string Route_Users_PasswordRecovery			= Route_Users + "/PasswordRecovery";

		public static string MediaType_JSON							= "application/json";
		public static string MediaType_FormURLEncoded				= "application/x-www-form-urlencoded";

		public static string Setting_UserEmail						= "useremail";
		public static string Setting_Password						= "password";
		public static string Setting_AccessToken					= "accesstoken";
		public static string Setting_UserId							= "userid";

		public static string Setting_About_ApplicationName			= "Athlosify 2019";
		public static string Setting_About_ApplicationVersion		= "1.1";
		public static string Setting_About_ApplicationDescription	= "This is an experimental application by DewaCorp's team and contributors " +
																		"to create a basic application to solve new challenges in the client applications including " +
																		"iOS mobile app, Android mobile app and website  " +
																		"plus a backend application including Web API.";
		public static string Setting_About_Owner					= "DewaCorp";
		public static string Setting_About_OwnerWebsiteURL			= "http://www.dewacorp.com";
		public static string Setting_About_Contributor				= "@Valdy, @JeffMorgan, @ArkiValdy, @AaronValdy";





	}
}
