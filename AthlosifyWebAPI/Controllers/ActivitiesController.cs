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
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Web;

namespace AthlosifyWebAPI.Controllers
{
	[Authorize]
	public class ActivitiesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/v1/Activities?sortfields=id&pagenumber=1&pagesize=10
		// GET: api/v1/Activities/Id,DateCreated/1/10
		// For the sortFields query string, the "-" sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Activities/{sortfields=Id}/{pagenumber=1}/{pagesize=10}", Name="GetActivities")]
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetActivities(string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort field invalid");
			}

			var activities = await db.Activities.Where(q => q.UserId == userId && 
															q.IsDeleted == false)
												.ToListAsync();

			if (activities == null)
			{
				return NotFound();
			}

			var noOfRecords = activities.Count();

			var activitiesDTO = await (db.Activities
										.Include(b => b.User)
										.Include(c => c.Category)
										.Where(q => q.UserId == userId && 
													q.IsDeleted == false)
										.ProjectTo< ActivityDTO>(automapperConfig)
										.AsQueryable()
										.ApplySort(sortfields)
										.Skip((pageNumber - 1) * pageSize).Take(pageSize)).ToListAsync();	

			var data = new
			{
				Metadata = new
				{
					TotalRecords = noOfRecords,
					CurrentPageSize = pageSize,
					CurrentPage = pageNumber,
					TotalPages = (int)Math.Ceiling(noOfRecords / (double)pageSize)
				},
				Results = activitiesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Category/1/Activities/?sortfields=id&pagenumber=1&pagesize=10
		// GET: api/v1/Category/1/Activities/Id,DateCreated/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Category/{categoryId}/Activities/{sortfields=Id}/{pagenumber=1}/{pagesize=10}", Name = "GetActivitiesByCategory")]
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetActivitiesByCategory(int categoryId, string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();
			//var iMapper = config.CreateMapper();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort property is incorrect");
			}

			var activities = await db.Activities.Where(q => q.UserId == userId && 
															q.CategoryId == categoryId && 
															q.IsDeleted == false)
												.ToListAsync();

			if (activities == null)
			{
				return NotFound();
			}

			var noOfRecords = activities.Count();

			var activitiesDTO = await (db.Activities
										.Include(b => b.User)
										.Include(c => c.Category)
										.Where(q => q.UserId == userId && q.CategoryId == categoryId && q.IsDeleted == false)
										.ProjectTo<ActivityDTO>(automapperConfig)
										.AsQueryable()
										.ApplySort(sortfields)
										.Skip((pageNumber - 1) * pageSize).Take(pageSize)).ToListAsync();

			var data = new
			{
				Metadata = new
				{
					TotalRecords = noOfRecords,
					CurrentPageSize = pageSize,
					CurrentPage = pageNumber,
					TotalPages = (int)Math.Ceiling(noOfRecords / (double)pageSize)
				},
				Results = activitiesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Activities/Search?keyword=&sort=id&pagenumber=1&pagesize=10
		// GET: api/v1/Activities/Search/bla/Id/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Activities/{keyword=}/{sortfields=id}/{pagenumber=1}/{pagesize=10}", Name = "SearchActivities")]
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> SearchActivities(string keyword, string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort property is incorrect");
			}

			var activities = await db.Activities.Where(q => q.UserId == userId && 
														q.IsDeleted == false)
												.Where(q => q.Name.Contains(keyword) || 
														q.Description.Contains(keyword))
												.ToListAsync();

			if (activities == null)
			{
				return NotFound();
			}

			var noOfRecords = activities.Count();

			var activitiesDTO = await (db.Activities.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId && 
																q.IsDeleted == false)
													.Where(q => q.Name.Contains(keyword) || 
																q.Description.Contains(keyword))
													.ProjectTo<ActivityDTO>(automapperConfig)
													.AsQueryable()
													.ApplySort(sortfields)
													.Skip((pageNumber - 1) * pageSize).Take(pageSize)).ToListAsync();

			var data = new
			{
				Metadata = new
				{
					TotalRecords = noOfRecords,
					CurrentPageSize = pageSize,
					CurrentPage = pageNumber,
					TotalPages = (int)Math.Ceiling(noOfRecords / (double)pageSize)
				},
				Results = activitiesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Activities/5
		[HttpGet]
        [ResponseType(typeof(ActivityDTO))]
		[Route("api/v1/Activities/{id}", Name = "GetActivity")]
		//[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetActivity(int id)
        {
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var activityDTO = await db.Activities.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId && 
																q.Id == id && 
																q.IsDeleted == false)
													.ProjectTo<ActivityDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			if (activityDTO == null)
            {
                return NotFound();
            }

            return Ok(activityDTO);
        }

		// PUT: api/v1/Activities/5
		[HttpPut]
		[Route("api/v1/Activities/{id}")]
		[ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutActivity(int id, ActivityDTO activityDTO)
        {
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			var category = db.Categories.SingleOrDefault(q => q.UserId == userId && 
																q.Name == activityDTO.CategoryName && 
																q.IsDeleted == false);

			if (category == null)
			{
				return BadRequest("Category not valid.");
			}

			if (id != activityDTO.Id)
            {
                return BadRequest();
            }

			var activity = await db.Activities.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id && 
																						q.IsDeleted == false);

			if (activity == null)
			{
				return NotFound();
			}
			
			if (userId != activity.UserId)
			{
				return BadRequest("No right access to update");
			}
			
			var updatedActivity = automapperConfig.CreateMapper().Map<ActivityDTO, Activity>(activityDTO);

			activity = db.Activities.Attach(updatedActivity);


			db.Entry(activity).State = EntityState.Modified;

            try
            {
				db.UserId = userId;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
					return BadRequest("The model is invalid");
				}
            }

            return Ok();
		}

        // POST: api/Activities
		[HttpPost]
		[Route("api/v1/Activities")]
		[ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> PostActivity(ActivityDTO activityDTO)
        {
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var category = db.Categories.SingleOrDefault(q => q.UserId == userId && 
																q.Name == activityDTO.CategoryName && 
																q.IsDeleted == false);

			if (category == null)
			{
				return BadRequest("Category not valid.");
			}

			var activity = automapperConfig.CreateMapper().Map<ActivityDTO, Activity>(activityDTO);


			db.Activities.Add(activity);

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return BadRequest("The model is invalid");
			}

			var newActivityDto = await db.Activities.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId && 
																q.Id == activity.Id && 
																q.IsDeleted == false)
													.ProjectTo<ActivityDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			return CreatedAtRoute("GetActivity", new { id = newActivityDto.Id }, newActivityDto);
		}

		public async Task CreateDefaultActivities(string userId)
		{
			var otherCategory = db.Categories.SingleOrDefault(q => q.UserId == userId &&
																q.Name == "Others" &&
																q.IsDeleted == false);

			var activities = new List<Activity>
			{
				new Activity { Name = "Welcome", UserId = userId, CategoryId = otherCategory.Id, NoOfMinutes = 0, Description = "Welcome to the activity section!"  }
			};

			foreach (Activity activity in activities)
			{
				db.Activities.Add(activity);
				await db.SaveChangesAsync();
			}
		}

		// DELETE: api/Activities/5
		[HttpDelete]
		[Route("api/v1/Activities/{id}")]
		[ResponseType(typeof(Activity))]
        public async Task<IHttpActionResult> DeleteActivity(int id)
        {
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var activity = await db.Activities
									.Where(q => q.UserId == userId && 
												q.Id == id && 
												q.IsDeleted == false)
									.FirstOrDefaultAsync();

			if (activity == null)
            {
                return NotFound();
            }

			if (userId != activity.UserId)
			{
				return BadRequest("No right access to delete");
			}

			// Check reference constraint
			try
			{
				// Not implemented
			}
			catch (Exception err)
			{
				return BadRequest(err.Message);
			}

			// Do the soft-delete
			db.IsSoftDelete = true;
			db.Entry(activity).State = EntityState.Modified;
			
			db.UserId = userId;
			await db.SaveChangesAsync();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ActivityExists(int id)
        {
            return db.Activities.Count(e => e.Id == id) > 0;
        }

		private bool IsOkPropertyValidate(string sort)
		{
			bool notFound = false;
			var lstSort = sort.Replace("-", "").Split(',');
			var propertyNames = typeof(ActivityDTO).GetProperties().Select(p => p.Name).ToList();
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