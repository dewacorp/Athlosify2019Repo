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
	public class NotesController : ApiController
	{
		private ApplicationDbContext db = new ApplicationDbContext();

		// GET: api/v1/Notes?sortfields=id&pagenumber=1&pagesize=10
		// GET: api/v1/Notes/Id,DateCreated/1/10
		// For the sortFields query string, the "-" sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Notes/{sortfields=Id}/{pagenumber=1}/{pagesize=10}", Name = "GetNotes")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetNotes(string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort field invalid");
			}

			var notes = await db.Notes.Where(q => q.UserId == userId &&
															q.IsDeleted == false)
												.ToListAsync();

			if (notes == null)
			{
				return NotFound();
			}

			var noOfRecords = notes.Count();

			var notesDTO = await (db.Notes
										.Include(b => b.User)
										.Include(c => c.Category)
										.Where(q => q.UserId == userId &&
													q.IsDeleted == false)
										.ProjectTo<NoteDTO>(automapperConfig)
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
				Results = notesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Category/1/Noates/?sortfields=id&pagenumber=1&pagesize=10
		// GET: api/v1/Category/1/Notes/Id,DateCreated/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Category/{categoryId}/Notes/{sortfields=Id}/{pagenumber=1}/{pagesize=10}", Name = "GetNotesByCategory")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetNotesByCategory(int categoryId, string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort property is incorrect");
			}

			var notes = await db.Notes.Where(q => q.UserId == userId &&
															q.CategoryId == categoryId &&
															q.IsDeleted == false)
												.ToListAsync();

			if (notes == null)
			{
				return NotFound();
			}

			var noOfRecords = notes.Count();

			var notesDTO = await (db.Notes
										.Include(b => b.User)
										.Include(c => c.Category)
										.Where(q => q.UserId == userId && q.CategoryId == categoryId && q.IsDeleted == false)
										.ProjectTo<NoteDTO>(automapperConfig)
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
				Results = notesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Notes/Search?keyword=&sort=id&pagenumber=1&pagesize=10
		// GET: api/v1/Notes/Search/bla/Id/1/10
		// For the sort query string, the - sign is allowed for decending
		[HttpGet]
		[Route("api/v1/Notes/Search/{keyword=}/{sortfields=id}/{pagenumber=1}/{pagesize=10}", Name = "SearchNotes")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> SearchNotes(string keyword, string sortfields, int pageNumber, int pageSize)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!IsOkPropertyValidate(sortfields))
			{
				return BadRequest("Sort property is incorrect");
			}

			var notes = await db.Notes.Where(q => q.UserId == userId &&
														q.IsDeleted == false)
												.Where(q => q.Subject.Contains(keyword) ||
														q.Content.Contains(keyword))
												.ToListAsync();

			if (notes == null)
			{
				return NotFound();
			}

			var noOfRecords = notes.Count();

			var notesDTO = await (db.Notes.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId &&
																q.IsDeleted == false)
													.Where(q => q.Subject.Contains(keyword) ||
																q.Content.Contains(keyword))
													.ProjectTo<NoteDTO>(automapperConfig)
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
				Results = notesDTO
			};

			return Ok(data);
		}

		// GET: api/v1/Notes/5
		[HttpGet]
		[ResponseType(typeof(NoteDTO))]
		[Route("api/v1/Notes/{id}", Name = "GetNote")]
		[CacheOutput(ClientTimeSpan = 60, ServerTimeSpan = 60)]
		public async Task<IHttpActionResult> GetNote(int id)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var noteDTO = await db.Notes.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId &&
																q.Id == id &&
																q.IsDeleted == false)
													.ProjectTo<NoteDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			if (noteDTO == null)
			{
				return NotFound();
			}

			return Ok(noteDTO);
		}

		// PUT: api/v1/Notes/5
		[HttpPut]
		[Route("api/v1/Notes/{id}")]
		[ResponseType(typeof(void))]
		public async Task<IHttpActionResult> PutNote(int id, NoteDTO noteDTO)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var category = db.Categories.SingleOrDefault(q => q.UserId == userId &&
																q.Name == noteDTO.CategoryName &&
																q.IsDeleted == false);

			if (category == null)
			{
				return BadRequest("Category not valid.");
			}

			if (id != noteDTO.Id)
			{
				return BadRequest();
			}

			var note = await db.Notes.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id &&
																						q.IsDeleted == false);

			if (note == null)
			{
				return NotFound();
			}

			if (userId != note.UserId)
			{
				return BadRequest("No right access to update");
			}

			var updatedNote = automapperConfig.CreateMapper().Map<NoteDTO, Note>(noteDTO);

			note = db.Notes.Attach(updatedNote);


			db.Entry(note).State = EntityState.Modified;

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!NoteExists(id))
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

		// POST: api/Notes
		[HttpPost]
		[Route("api/v1/Notes")]
		[ResponseType(typeof(Note))]
		public async Task<IHttpActionResult> PostNote(NoteDTO noteDTO)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var category = db.Categories.SingleOrDefault(q => q.UserId == userId &&
																q.Name == noteDTO.CategoryName &&
																q.IsDeleted == false);

			if (category == null)
			{
				return BadRequest("Category not valid.");
			}

			var note = automapperConfig.CreateMapper().Map<NoteDTO, Note>(noteDTO);


			db.Notes.Add(note);

			try
			{
				db.UserId = userId;
				await db.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				return BadRequest("The model is invalid");
			}

			var newNoteDto = await db.Notes.Include(b => b.User)
													.Include(c => c.Category)
													.Where(q => q.UserId == userId &&
																q.Id == note.Id &&
																q.IsDeleted == false)
													.ProjectTo<NoteDTO>(automapperConfig)
													.FirstOrDefaultAsync();

			return CreatedAtRoute("GetNote", new { id = newNoteDto.Id }, newNoteDto);
		}

		// DELETE: api/Notes/5
		[HttpDelete]
		[Route("api/v1/Notes/{id}")]
		[ResponseType(typeof(Note))]
		public async Task<IHttpActionResult> DeleteNote(int id)
		{
			var automapperConfig = new AutoMapperConfig().Configure();

			string userId = User.Identity.GetUserId();

			var note = await db.Notes
									.Where(q => q.UserId == userId &&
												q.Id == id &&
												q.IsDeleted == false)
									.FirstOrDefaultAsync();

			if (note == null)
			{
				return NotFound();
			}

			if (userId != note.UserId)
			{
				return BadRequest("No right access to delete");
			}

			// Do the soft delete
			db.IsSoftDelete = true;
			db.Entry(note).State = EntityState.Modified;

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

		private bool NoteExists(int id)
		{
			return db.Notes.Count(e => e.Id == id) > 0;
		}

		private bool IsOkPropertyValidate(string sort)
		{
			bool notFound = false;
			var lstSort = sort.Replace("-", "").Split(',');
			var propertyNames = typeof(NoteDTO).GetProperties().Select(p => p.Name).ToList();
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