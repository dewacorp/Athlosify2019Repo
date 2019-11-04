using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class NoteShare : TrackableEntry
	{
		[DataMember]
		public int? NoteId { get; set; }

		[DataMember]
		public string UserId { get; set; }

		[DataMember]
		public virtual Note Note { get; set; }

		[DataMember]
		public virtual ApplicationUser User { get; set; }

	}
}