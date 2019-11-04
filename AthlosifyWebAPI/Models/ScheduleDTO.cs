using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AthlosifyWebAPI.Models
{
	public class ScheduleDTO : IBaseDTO
	{
		public int Id { get; set; }

		public string OwnerName { get; set; }

		public string CategoryName { get; set; }

		public string Subject { get; set; }

		public string Location { get; set; }

		public string Description { get; set; }

		public DateTime StartDateTime { get; set; }

		public DateTime EndDateTime { get; set; }

		public bool IsAllDayEvent { get; set; }

	}
}