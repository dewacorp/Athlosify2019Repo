using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class UserProfileDTO : IBaseDTO
	{
		public int Id { get; set; }

		public string OwnerId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Gender { get; set; }

		public DateTime? BirthDate { get; set; }

		public string Suburb { get; set; }

		public string State { get; set; }

		public string Postcode { get; set; }
		public string Country { get; set; }

		public float? Handicap { get; set; }

		public string ProfileDetail { get; set; }

		public string ClubName { get; set; }

		public string ProfilePhoto { get; set; }

	}
}