using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class IBaseDTO
	{
		public DateTime? CreatedDate { get; set; }

		public DateTime? ModifiedDate { get; set; }


	}
}