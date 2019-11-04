using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Note : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public int? CategoryId { get; set; }

		[DataMember]
		[Required]
		[MaxLength(64)]
		public string Subject { get; set; }

		[DataMember]
		[MaxLength(1024)]
		public string Content { get; set; }

		public virtual ApplicationUser User { get; set; }

		[DataMember]
		public virtual Category Category { get; set; }

	}
}