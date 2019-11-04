using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthlosifyWebAPI.Models
{
	[DataContract]
	public class Activity : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public int? CategoryId { get; set; }

		[DataMember]
		[Required]
		[MaxLength(64)]
		public string Name { get; set; }

		[DataMember]
		[MaxLength(256)]
		public string Description { get; set; }

		[DataMember]
		[Required]
		public int NoOfMinutes { get; set; }

		public virtual ApplicationUser User { get; set; }

		[DataMember]
		public virtual Category Category { get; set; }


	}
}