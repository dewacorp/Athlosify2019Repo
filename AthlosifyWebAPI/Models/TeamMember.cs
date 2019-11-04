using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class TeamMember : TrackableEntry
	{
		[DataMember]
		public int? TeamId { get; set; }

		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		[Required]
		[MaxLength(32)]
		public string UserType { get; set; }

		[DataMember]
		[MaxLength(256)]
		public string Description { get; set; }

		public virtual Team Team { get; set; }

		public virtual ApplicationUser User { get; set; }

	}
}