using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
    public class TokenResponse
    {
		public string access_token { get; set; }

		public string token_type { get; set; }

		public int expires_in { get; set; }

		public string userName { get; set; }

		public string expires { get; set; }

		public string userID { get; set; }

	}
}
