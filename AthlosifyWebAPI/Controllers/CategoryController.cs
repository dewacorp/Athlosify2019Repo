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
	public class CategoriesController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/v1/Categories
		[HttpGet]
		[Route("api/v1/Categories", Name = "GetCategories")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetCategories()
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var categories = await db.Categories.Where(q => q.UserId == userId &&
															q.IsDeleted == false)
												.ToListAsync();

			if (categories == null)
			{
				return NotFound();
			}

			var noOfRecords = categories.Count();

			var categoriesDTO = await (db.Categories
										.Include(b => b.User)
										.Include(c => c.Parent)
										.Where(q => q.UserId == userId &&
													q.IsDeleted == false)
										.ProjectTo<CategoryDTO>(automapperConfig)
										.AsQueryable()).ToListAsync();

			var data = new
			{
				Metadata = new
				{
					TotalRecords = noOfRecords,
					CurrentPageSize = 0,
					CurrentPage = 0,
					TotalPages = 0
				},
				Results = categoriesDTO
			};

			return Ok(data);
		}


		// GET: api/v1/Categories/5
		[HttpGet]
		[ResponseType(typeof(CategoryDTO))]
		[Route("api/v1/Categories/{id}", Name = "GetCategory")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetCategory(int id)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var categoryDTO = await db.Categories.Include(b => b.User)
													.Include(c => c.Parent)
													.Where(q => q.UserId == userId &&
																q.Id == id &&
																q.IsDeleted == false)
													.ProjectTo<CategoryDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			if (categoryDTO == null)
			{
				return NotFound();
			}

			return Ok(categoryDTO);
		}

		// PUT: api/v1/Categories/5
		[HttpPut]
		[Route("api/v1/Categories/{id}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutCategory(int id, CategoryDTO categoryDTO)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var parentCategory = db.Categories.SingleOrDefault(q => q.UserId == userId &&
																q.Name == categoryDTO.ParentName &&
																q.IsDeleted == false);

			if (parentCategory == null)
			{
				return BadRequest("Parent category not valid.");
			}

			if (id != categoryDTO.Id)
			{
				return BadRequest();
			}

			var category = await db.Categories.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id &&
																						q.IsDeleted == false);

			if (category == null)
			{
				return NotFound();
			}

			if (userId != category.UserId)
			{
				return BadRequest("No right access to update");
			}

			var updatedCategory = automapperConfig.CreateMapper().Map<CategoryDTO, Category>(categoryDTO);

			category = db.Categories.Attach(updatedCategory);


			db.Entry(category).State = EntityState.Modified;

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
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

		// POST: api/Categories
		[HttpPost]
		[Route("api/v1/Categories")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> PostCategory(CategoryDTO categoryDTO)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var parentCategory = db.Categories.SingleOrDefault(q => q.UserId == userId &&
																q.Name == categoryDTO.ParentName &&
																q.IsDeleted == false);

			if (parentCategory == null)
			{
				return BadRequest("Category not valid.");
			}

			var category = automapperConfig.CreateMapper().Map<CategoryDTO, Category>(categoryDTO);


			db.Categories.Add(category);

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return BadRequest("The model is invalid");
			}

			var newCategoryDto = await db.Categories.Include(b => b.User)
													.Include(c => c.Parent)
													.Where(q => q.UserId == userId &&
																q.Id == category.Id &&
																q.IsDeleted == false)
													.ProjectTo<CategoryDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			return CreatedAtRoute("GetCategory", new { id = newCategoryDto.Id }, newCategoryDto);
		}

		public async Task CreateDefaultCategories(string userId)
		{
			var categories = new List<Category>
			{
				new Category { Name = "Technique", UserId = userId, CreatedDate = DateTime.UtcNow , ModifiedDate = null },
				new Category { Name = "Mental", UserId = userId, CreatedDate = DateTime.UtcNow , ModifiedDate = null },
				new Category { Name = "Physical", UserId = userId, CreatedDate = DateTime.UtcNow , ModifiedDate = null },
				new Category { Name = "Nutrient", UserId = userId, CreatedDate = DateTime.UtcNow , ModifiedDate = null },
				new Category { Name = "Others", UserId = userId, CreatedDate = DateTime.UtcNow , ModifiedDate = null },
			};

			foreach (Category category in categories)
			{
				db.Categories.Add(category);
				await db.SaveChangesAsync();
			}
		}

		// DELETE: api/Categories/5
		[HttpDelete]
		[Route("api/v1/Categories/{id}")]
		[ResponseType(typeof(Category))]
		public async Task<IHttpActionResult> DeleteCategory(int id)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var category = await db.Categories
									.Where(q => q.UserId == userId &&
												q.Id == id &&
												q.IsDeleted == false)
									.FirstOrDefaultAsync();

			if (category == null)
			{
				return NotFound();
			}

			if (userId != category.UserId)
			{
				return BadRequest("No right access to delete");
			}

		
			// Check reference constraint
			try
			{
				ItemDependant<Activity>(id, m => m.CategoryId == id, s => s.Name);
				ItemDependant<Note>(id, m => m.CategoryId == id, s => s.Subject);
			}
			catch(Exception err)
			{
				return BadRequest(err.Message);
			}

			// Do the soft-delete
			db.IsSoftDelete = true;
			db.Entry(category).State = EntityState.Modified;

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

		private bool CategoryExists(int id)
		{
			return db.Categories.Count(e => e.Id == id) > 0;
		}

		private bool IsOkPropertyValidate(string sort)
		{
			bool notFound = false;
			var lstSort = sort.Replace("-", "").Split(',');
			var propertyNames = typeof(CategoryDTO).GetProperties().Select(p => p.Name).ToList();
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