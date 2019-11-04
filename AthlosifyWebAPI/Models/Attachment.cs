using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Attachment : TrackableEntry
	{
		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public int NoteId { get; set; }

		[DataMember]
		[MaxLength(128)]
		[Required]
		public string FileName { get; set; }

		[DataMember]
		[MaxLength(256)]
		[Required]
		public string FilePath { get; set; }

		public virtual Note Note { get; set; }

		[DataMember]
		public virtual ApplicationUser User { get; set; }

	}
}