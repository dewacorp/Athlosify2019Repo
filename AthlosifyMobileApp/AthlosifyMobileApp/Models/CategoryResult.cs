using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
	public class CategoryResult
	{
		public Metadata Metadata { get; set; }
		public List<Category> Results { get; set; }
	}
}
