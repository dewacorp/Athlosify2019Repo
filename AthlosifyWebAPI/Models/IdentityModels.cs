using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AthlosifyWebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.UserProfile> UserProfiles { get; set; }
		
		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Activity> Activities { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Note> Notes { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Category> Categories { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Team> Teams { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.TeamMember> TeamMembers { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Comment> Comments { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Message> Messages { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.NoteShare> NoteShares { get; set; }

		public System.Data.Entity.DbSet<AthlosifyWebAPI.Models.Schedule> Schedules { get; set; }

		public string UserId { get; set; }

		public bool IsSoftDelete { get; set; }


		public override int SaveChanges()
		{
			this.ChangeTracker.DetectChanges();

			// Added State
			var added = this.ChangeTracker.Entries()
						.Where(t => t.State == EntityState.Added)
						.Select(t => t.Entity)
						.ToArray();

			foreach (var entity in added)
			{
				if (entity is ITrack)
				{
					var track = entity as ITrack;
					track.CreatedDate = DateTime.UtcNow;
					track.CreatedBy = UserId;
					track.IsDeleted = false;
				}
			}

			// Modified State + soft delete
			var modified = this.ChangeTracker.Entries()
						.Where(t => t.State == EntityState.Modified)
						.Select(t => t.Entity)
						.ToArray();

			foreach (var entity in modified)
			{
				if (entity is ITrack)
				{
					var track = entity as ITrack;
					Entry(track).Property(x => x.CreatedDate).IsModified = false;
					Entry(track).Property(x => x.CreatedBy).IsModified = false;

					if (!IsSoftDelete)
					{
						track.ModifiedDate = DateTime.UtcNow;
						track.ModifiedBy = UserId;
					}
					else
					{
						track.IsDeleted = true;
						track.DeletedDate = DateTime.UtcNow;
						track.DeletedBy = UserId;
					}
				}
			}

			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync()
		{
			this.ChangeTracker.DetectChanges();

			// Added State
			var added = this.ChangeTracker.Entries()
						.Where(t => t.State == EntityState.Added)
						.Select(t => t.Entity)
						.ToArray();

			foreach (var entity in added)
			{
				if (entity is ITrack)
				{
					var track = entity as ITrack;
					track.CreatedDate = DateTime.UtcNow;
					track.CreatedBy = UserId;
				}
			}

			// Modified State
			var modified = this.ChangeTracker.Entries()
						.Where(t => t.State == EntityState.Modified)
						.Select(t => t.Entity)
						.ToArray();

			foreach (var entity in modified)
			{
				if (entity is ITrack)
				{
					var track = entity as ITrack;
					Entry(track).Property(x => x.CreatedDate).IsModified = false;
					Entry(track).Property(x => x.CreatedBy).IsModified = false;

					if (!IsSoftDelete)
					{
						track.ModifiedDate = DateTime.UtcNow;
						track.ModifiedBy = UserId;
					}
					else
					{
						track.IsDeleted = true;
						track.DeletedDate = DateTime.UtcNow;
						track.DeletedBy = UserId;
					}
				}
			}

			return await base.SaveChangesAsync();
		}
	}
}