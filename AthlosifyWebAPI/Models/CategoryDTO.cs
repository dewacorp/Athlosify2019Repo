using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AthlosifyWebAPI.Models
{
	public class CategoryDTO : IBaseDTO
	{
		public int Id { get; set; }

		public string OwnerId { get; set; }

		public int ParentId { get; set; }

		public string ParentName { get; set; }

		public string Name { get; set; }

	}
}