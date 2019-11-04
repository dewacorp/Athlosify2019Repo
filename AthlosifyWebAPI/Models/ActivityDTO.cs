using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class ActivityDTO : IBaseDTO
	{
		public int Id { get; set; }

		public string OwnerId { get; set; }

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public int NoOfMinutes { get; set; }

	}
}