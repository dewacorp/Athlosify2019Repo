using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public interface ITrack
	{
		int Id {get; set;}


		string CreatedBy {get; set;}

		DateTime CreatedDate {get; set;}

		string ModifiedBy {get; set;}

		DateTime? ModifiedDate {get; set;}

		Boolean IsDeleted { get; set; }

		string DeletedBy { get; set; }

		DateTime? DeletedDate { get; set; }

	}
}