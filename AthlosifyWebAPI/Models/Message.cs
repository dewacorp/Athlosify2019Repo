using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class Message : TrackableEntry
	{
		[DataMember]
		public int? ParentId { get; set; }

		[DataMember]
		public string SenderUserId { get; set; }

		[DataMember]
		public string ReceiverUserId { get; set; }

		[DataMember]
		[MaxLength(512)]
		[Required]
		public string Content { get; set; }

		public virtual Message Parent { get; set; }

		[DataMember]
		public virtual ApplicationUser SenderUser { get; set; }

		[DataMember]
		public virtual ApplicationUser ReceiverUser { get; set; }

		[DataMember]
		public virtual ICollection<Message> Children { get; set; }

	}
}