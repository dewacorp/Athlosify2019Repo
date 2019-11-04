using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AthlosifyWebAPI.Models;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;

namespace AthlosifyWebAPI.Controllers
{
    public class SchedulesController : ApiController
    {
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/Schedules?sort=id&pagenumber=1&pagesize=10
		// GET: api/Schedules/Id,DateCreated/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/Schedules/{sort=Id}/{pagenumber=1}/{pagesize=10}")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetSchedules(string sort, int pageNumber, int pageSize)
		{
			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sort))
			{
				return BadRequest("Sort property is incorrect");
			}

			var activities = await (db.Schedules.Where(q => q.UserId == userId)
										.AsQueryable()
										.ApplySort(sort)
										.Skip((pageNumber - 1) * pageSize).Take(pageSize)).ToListAsync();

			return Ok(activities);
		}

		// GET: api/Schedules/search?keyword=&sort=id&pagenumber=1&pagesize=10
		// GET: api/Schedules/search/bla/Id/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/Schedules/search/{keyword=}/{sort=id}/{pagenumber=1}/{pagesize=10}")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> SearchScheduless(string keyword, string sort, int pageNumber, int pageSize)
		{
			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sort))
			{
				return BadRequest("Sort property is incorrect");
			}

			var activities = await (db.Activities.Where(q => q.UserId == userId)
										.Where(q => q.Name.Contains(keyword) || q.Description.Contains(keyword))
										.AsQueryable()
										.ApplySort(sort)
										.Skip((pageNumber - 1) * pageSize).Take(pageSize)).ToListAsync();

			return Ok(activities);
		}

		// GET: api/Schedules/5
		[HttpGet]
		[ResponseType(typeof(Schedule))]
		[Route("api/Schedules/{id}")]
		public async Task<IHttpActionResult> GetSchedule(int id)
		{
			string userId = User.Identity.GetUserId();

			Schedule schedule = await db.Schedules.FirstOrDefaultAsync(q => q.UserId == userId && q.Id == id);

			if (schedule == null)
			{
				return NotFound();
			}

			return Ok(schedule);
		}

		// PUT: api/Activities/5
		[HttpPut]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutSchedule(int id, Schedule schedule)
		{
			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != schedule.Id)
			{
				return BadRequest();
			}

			schedule = await db.Schedules.FirstOrDefaultAsync(q => q.Id == id);
			if (userId != schedule.UserId)
			{
				return BadRequest("No right access to update");
			}

			db.Entry(schedule).State = EntityState.Modified;

			try
			{
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ScheduleExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Schedules
		[HttpPost]
		[ResponseType(typeof(Schedule))]
		public async Task<IHttpActionResult> PostSchdule(Schedule schedule)
		{
			string userId = User.Identity.GetUserId();
			schedule.UserId = userId;

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Schedules.Add(schedule);
			await db.SaveChangesAsync();

			return CreatedAtRoute("DefaultApi", new { id = schedule.Id }, schedule);
		}

		// DELETE: api/Schedules/5
		[HttpDelete]
		[ResponseType(typeof(Schedule))]
		public async Task<IHttpActionResult> DeleteSchedule(int id)
		{
			string userId = User.Identity.GetUserId();

			Schedule schedule = await db.Schedules.FindAsync(id);
			if (schedule == null)
			{
				return NotFound();
			}

			if (userId != schedule.UserId)
			{
				return BadRequest("No right access to delete");
			}

			db.Schedules.Remove(schedule);
			await db.SaveChangesAsync();

			return Ok(schedule);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool ScheduleExists(int id)
		{
			return db.Schedules.Count(e => e.Id == id) > 0;
		}

		private bool IsOkPropertyValidate(string sort)
		{
			bool notFound = false;
			var lstSort = sort.Replace("-", "").Split(',');
			var propertyNames = typeof(Schedule).GetProperties().Select(p => p.Name).ToList();
			foreach (var sortBy in lstSort)
			{
				if (propertyNames.IndexOf(sortBy) < 0)
					notFound = true;
				else
					notFound = false;
			}

			if (notFound)
				return false;
			else
				return true;
		}
	}
}