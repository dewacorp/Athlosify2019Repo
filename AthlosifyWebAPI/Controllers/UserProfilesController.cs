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
using System.Data.Entity.Migrations;

namespace AthlosifyWebAPI.Controllers
{
	[Authorize]
	public class UserProfilesController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		
		// GET: api/v1/UserProfiles/5
		[HttpGet]
		[ResponseType(typeof(UserProfileDTO))]
		[Route("api/v1/UserProfiles/{id}", Name = "GetUserProfiles")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetCategory(int id)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var userProfileDTO = await db.UserProfiles.Include(b => b.User)
													.Where(q => q.UserId == userId &&
																q.Id == id &&
																q.IsDeleted == false)
													.ProjectTo<UserProfileDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			if (userProfileDTO == null)
			{
				return NotFound();
			}

			return Ok(userProfileDTO);
		}

		// PUT: api/v1/UserProfiles/5
		[HttpPut]
		[Route("api/v1/Categories/{id}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutUserProfile(int id, UserProfileDTO userProfileDTO)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != userProfileDTO.Id)
			{
				return BadRequest();
			}

			var userProfile = await db.UserProfiles.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id &&
																						q.IsDeleted == false);

			if (userProfile == null)
			{
				return NotFound();
			}

			if (userId != userProfile.UserId)
			{
				return BadRequest("No right access to update");
			}

			var updatedPlayer = automapperConfig.CreateMapper().Map<UserProfileDTO, UserProfile>(userProfileDTO);

			userProfile = db.UserProfiles.Attach(updatedPlayer);


			db.Entry(userProfile).State = EntityState.Modified;

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!UserProfileExists(id))
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

		public async Task CreateUserProfile(string userId, RegisterBindingModel model)
		{
			UserProfile player = new UserProfile
			{
				UserId = userId,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Handicap = model.Handicap
			};
				
			db.UserProfiles.Add(player);
			await db.SaveChangesAsync();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool UserProfileExists(int id)
		{
			return db.UserProfiles.Count(e => e.Id == id) > 0;
		}

		public void ItemDependant<TEntity>(int id,
						Expression<Func<TEntity, bool>> condition,
						Expression<Func<TEntity, string>> select) where TEntity : TrackableEntry
		{
			var foundItems = db.Set<TEntity>().Where(x => x.IsDeleted == false).Where(condition);

			if (foundItems.Any())
			{
				throw new Exception($"Conflicted with the reference constraint. Dependent items: {foundItems.ElementType.Name} - {foundItems.Select(select).ToList().Aggregate((i, j) => i + ", " + j)}");
			}
		}
	}
}