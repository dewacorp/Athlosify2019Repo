using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	[DataContract]
	public class UserProfile : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string FirstName { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string LastName { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string Gender { get; set; }

		[DataMember]
		public DateTime? BirthDate { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string Suburb { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string State { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string Postcode { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string Country { get; set; }

		[DataMember]
		public float? Handicap { get; set; }

		[DataMember]
		[MaxLength(512)]
		public string ProfileDetail { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string ClubName { get; set; }

		[DataMember]
		[MaxLength(128)]
		public string ProfilePhoto { get; set; }

		public virtual ApplicationUser User { get; set; }

	}
}