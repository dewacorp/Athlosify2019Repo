using System;
using System.Collections.Generic;
using System.Text;

namespace AthlosifyMobileApp.Models
{
	public class ActivityResult
	{
		public Metadata Metadata { get; set; }
		public List<Activity> Results { get; set; }
	}
}
