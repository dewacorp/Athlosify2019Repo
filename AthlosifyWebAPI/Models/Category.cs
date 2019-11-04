using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Category : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		[Required]
		[MaxLength(64)]
		public String Name { get; set; }

		[DataMember]
		[MaxLength(128)]
		public String Description { get; set; }

		public virtual Category Parent { get; set; }

		public virtual ApplicationUser User { get; set; }

		[DataMember]
		public virtual ICollection<Category> Children { get; set; }


	}
}