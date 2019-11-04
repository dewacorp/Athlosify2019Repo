using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Comment : TrackableEntry
	{
		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public int NoteId { get; set; }

		[DataMember]
		[MaxLength(256)]
		[Required]
		public string Content { get; set; }

		public virtual Note Note { get; set; }

		public virtual Comment Parent { get; set; }

		[DataMember]
		public virtual ApplicationUser User { get; set; }

		[DataMember]
		public virtual ICollection<Comment> Children { get; set; }


	}
}