using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class NoteDTO : IBaseDTO
	{
		public int Id { get; set; }

		public string OwnerId { get; set; }

		public string OwnerName { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Subject { get; set; }

		public string Content { get; set; }

	}
}