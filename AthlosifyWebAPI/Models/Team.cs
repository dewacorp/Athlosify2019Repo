using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Team : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		[MaxLength(64)]
		public string Name { get; set; }

		[DataMember]
		[MaxLength(256)]
		public string Description { get; set; }

		[DataMember]
		[MaxLength(128)]
		public string TeamPhoto { get; set; }

		public virtual ApplicationUser User { get; set; }


	}
}