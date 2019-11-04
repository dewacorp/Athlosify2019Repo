using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
    public class Category
    {
		public int Id { get; set; }
		public string OwnerId { get; set; }
		public int ParentId { get; set; }
		public string ParentName { get; set; }
		public string Name { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? ModifiedDate { get; set; }
	}
}
