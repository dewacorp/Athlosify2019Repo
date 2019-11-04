using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
    public class Metadata
    {
		public int TotalRecords { get; set; }
		public int CurrentPageSize { get; set; }
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
	}
}
