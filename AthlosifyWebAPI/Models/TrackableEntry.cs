using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public abstract class TrackableEntry : ITrack
	{
		[Key]
		public int Id { get; set; }

		public string CreatedBy { get; set; }

		public DateTime CreatedDate { get; set; }

		public string ModifiedBy { get; set; }

		public DateTime? ModifiedDate { get; set; }


		public Boolean IsDeleted { get; set; }

		public string DeletedBy { get; set; }

		public DateTime? DeletedDate { get; set; }
	}
}