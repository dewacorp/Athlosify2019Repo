namespace AthlosifyWebAPI.Migrations
{
	using AthlosifyWebAPI.Models;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;

	internal sealed class Configuration : DbMigrationsConfiguration<AthlosifyWebAPI.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(AthlosifyWebAPI.Models.ApplicationDbContext context)
		{
			var userStore = new UserStore<ApplicationUser>(context);
			var userManager = new UserManager<ApplicationUser>(userStore);

			// Create roles

			string[] roles = new string[] { "Administrator", "Player", "Coach", "Parent" };

			var roleStore = new RoleStore<IdentityRole>(context);
			var roleManager = new RoleManager<IdentityRole>(roleStore);
			
			foreach (string role in roles)
			{ 
				var applicationRoleAdministrator = new IdentityRole(role);
				if (!roleManager.RoleExists(applicationRoleAdministrator.Name))
				{
					roleManager.Create(applicationRoleAdministrator);
				}
			}

			// Create admin user

			var adminUser = userManager.FindByEmail("admin@athlosify.com");

			if (adminUser == null)
			{
				adminUser = new ApplicationUser { Email = "admin@athlosify.com", UserName = "admin@athlosify.com" };
				userManager.Create(adminUser, "p@ssw0rD");

				userManager.AddToRoles(adminUser.Id, "Administrator");
			}

			// Create user 1

			var userId1 = "";
			var user1 = userManager.FindByEmail("arkiv@dewacorp.com");
			if (user1 == null)
			{
				user1 = new ApplicationUser { Email = "arkiv@dewacorp.com", UserName = "arkiv@dewacorp.com" };
				userManager.Create(user1, "p@ssw0rD");

				userManager.AddToRoles(user1.Id, "Player");
			}

			// Create user 2

			var userId2 = "";
			var user2 = userManager.FindByEmail("aaronv@dewacorp.com");
			if (user2 == null)
			{
				user2 = new ApplicationUser { Email = "aaronv@dewacorp.com", UserName = "aaronv@dewacorp.com" };
				userManager.Create(user2, "p@ssw0rD");

				userManager.AddToRoles(user2.Id, "Player");
			}

			// Create user 3

			var userId3 = "";
			var user3 = userManager.FindByEmail("dennis@dennishuttony.com");
			if (user3 == null)
			{
				user3 = new ApplicationUser { Email = "dennis@dennishuttony.com", UserName = "dennis@dennishuttony.com" };
				userManager.Create(user3, "p@ssw0rD");

				userManager.AddToRoles(user3.Id, "Coach");
			}

			// Create user 4

			var userId4 = "";
			var user4 = userManager.FindByEmail("matt@mindoriumy.com");
			if (user4 == null)
			{
				user4 = new ApplicationUser { Email = "matt@mindoriumy.com", UserName = "matt@mindoriumy.com" };
				userManager.Create(user4, "p@ssw0rD");
				userId4 = user4.Id;

				userManager.AddToRoles(user4.Id, "Coach");
			}

			var userProfiles = new List<UserProfile>
			{
				new UserProfile { UserId = user1.Id, FirstName = "Arki", LastName = "Valdy", Handicap = (float)2.0, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new UserProfile { UserId = user2.Id, FirstName = "Aaron", LastName = "Valdy", Handicap = (float)11.0, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null },
				new UserProfile { UserId = user3.Id, FirstName = "Dennis", LastName = "Huttony", CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new UserProfile { UserId = user4.Id, FirstName = "Matt", LastName = "Howy", CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null }

			};
			userProfiles.ForEach(s => context.UserProfiles.AddOrUpdate(p => p.FirstName, s));
			context.SaveChanges();

			var teams = new List<Team>
			{
				new Team { Name = "Arki Valdy's Team", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Team { Name = "Aaron Valdy's Team", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null }
			};
			teams.ForEach(s => context.Teams.AddOrUpdate(p => p.Name, s));
			context.SaveChanges();

			var teamMembers = new List<TeamMember>
			{
				new TeamMember { TeamId = teams.Find(x => x.Name == "Arki Valdy's Team").Id, UserId = user1.Id,  UserType = "Player", CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new TeamMember { TeamId = teams.Find(x => x.Name == "Arki Valdy's Team").Id, UserId = user3.Id,  UserType = "Coach",  CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new TeamMember { TeamId = teams.Find(x => x.Name == "Arki Valdy's Team").Id, UserId = user4.Id,  UserType = "Coach",     CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },

				new TeamMember { TeamId = teams.Find(x => x.Name == "Aaron Valdy's Team").Id, UserId = user2.Id, UserType = "Player",           CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new TeamMember { TeamId = teams.Find(x => x.Name == "Aaron Valdy's Team").Id, UserId = user3.Id, UserType = "Coach",  CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null }
			};
			teamMembers.ForEach(s => context.TeamMembers.AddOrUpdate(p => p.UserId, s));
			context.SaveChanges();


			var categories = new List<Category>
			{
				new Category { Name = "User 1 Technique", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Category { Name = "User 1 Mental", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null },
				new Category { Name = "User 1 Physical", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-03") , ModifiedDate = null },
				new Category { Name = "User 1 Nutrient", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-04") , ModifiedDate = null },
				new Category { Name = "User 1 Others", UserId = user1.Id, CreatedDate = DateTime.Parse("2019-09-05") , ModifiedDate = null },

				new Category { Name = "User 2 Technique", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Category { Name = "User 2 Mental", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null },
				new Category { Name = "User 2 Physical", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-03") , ModifiedDate = null },
				new Category { Name = "User 2 Nutrient", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-04") , ModifiedDate = null },
				new Category { Name = "User 2 Others", UserId = user2.Id, CreatedDate = DateTime.Parse("2019-09-05") , ModifiedDate = null }
			};
			categories.ForEach(s => context.Categories.AddOrUpdate(p => p.Name, s));
			context.SaveChanges();

			var notes = new List<Note>
			{
				new Note { Subject = "User 1 Mental Lesson Note", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Mental").Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Note { Subject = "User 1 Technique Lesson Note", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null },
				new Note { Subject = "User 1 Exercise Lesson Note", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Physical").Id, CreatedDate = DateTime.Parse("2019-09-03") , ModifiedDate = null },
				new Note { Subject = "User 1 Nutrient Note", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-04") , ModifiedDate = null },
				new Note { Subject = "User 1 Mental Lesson Note 2", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Mental").Id, CreatedDate = DateTime.Parse("2019-09-05") , ModifiedDate = null },
				new Note { Subject = "User 1 Technique Lesson Note 2", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-06") , ModifiedDate = null },
				new Note { Subject = "User 1 Exercise Lesson Note 2", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Physical").Id, CreatedDate = DateTime.Parse("2019-09-07") , ModifiedDate = null },
				new Note { Subject = "User 1 Nutrient Note 2", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-08") , ModifiedDate = null },
				new Note { Subject = "User 1 Mental Lesson Note 3", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Mental").Id, CreatedDate = DateTime.Parse("2019-09-09") , ModifiedDate = null },
				new Note { Subject = "User 1 Technique Lesson Note 3", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-10") , ModifiedDate = null },
				new Note { Subject = "User 1 Exercise Lesson Note 3", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Physical").Id, CreatedDate = DateTime.Parse("2019-09-11") , ModifiedDate = null },
				new Note { Subject = "User 1 Nutrient Note 3", Content = "Content here", UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-12") , ModifiedDate = null },

				new Note { Subject = "User 2 Mental Lesson Note", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Mental").Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Note { Subject = "User 2 Technique Lesson Note", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null },
				new Note { Subject = "User 2 Exercise Lesson Note", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Physical").Id, CreatedDate = DateTime.Parse("2019-09-03") , ModifiedDate = null },
				new Note { Subject = "User 2 Nutrient Note", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-04") , ModifiedDate = null },
				new Note { Subject = "User 2 Mental Lesson Note 2", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Mental").Id, CreatedDate = DateTime.Parse("2019-09-05") , ModifiedDate = null },
				new Note { Subject = "User 2 Technique Lesson Note 2", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-06") , ModifiedDate = null },
				new Note { Subject = "User 2 Exercise Lesson Note 2", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Physical").Id, CreatedDate = DateTime.Parse("2019-09-07") , ModifiedDate = null },
				new Note { Subject = "User 2 Nutrient Note 2", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-08") , ModifiedDate = null},
				new Note { Subject = "User 2 Mental Lesson Note 3", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Mental").Id, CreatedDate = DateTime.Parse("2019-09-09") , ModifiedDate = null },
				new Note { Subject = "User 2 Technique Lesson Note 3", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-10") , ModifiedDate = null },
				new Note { Subject = "User 2 Exercise Lesson Note 3", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Physical").Id, CreatedDate = DateTime.Parse("2019-09-11") , ModifiedDate = null },
				new Note { Subject = "User 2 Nutrient Note 3", Content = "Content here", UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Nutrient").Id, CreatedDate = DateTime.Parse("2019-09-12") , ModifiedDate = null }

			};
			notes.ForEach(s => context.Notes.AddOrUpdate(p => p.Subject, s));
			context.SaveChanges();

			var comments = new List<Comment>
			{
				new Comment { NoteId = notes.Find(x => x.Subject == "User 1 Technique Lesson Note").Id, Content = "Good work Arki", UserId = user3.Id, CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Comment { NoteId = notes.Find(x => x.Subject == "User 2 Technique Lesson Note").Id, Content = "Goof work Aaron", UserId = user3.Id, CreatedDate = DateTime.Parse("2019-09-02") , ModifiedDate = null }
			};
			comments.ForEach(s => context.Comments.AddOrUpdate(p => p.Content, s));
			context.SaveChanges();

			var activities = new List<Activity>
			{
				new Activity { Name = "User 1 Speedstick",                   NoOfMinutes = 15, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Physical").Id,  CreatedDate = DateTime.Parse("2019-09-01"), ModifiedDate = null },
				new Activity { Name = "User 1 Swing Fix",                    NoOfMinutes = 60, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-02"), ModifiedDate = null },
				new Activity { Name = "User 1 Wedge Gapping",                NoOfMinutes = 30, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-03"), ModifiedDate = null },
				new Activity { Name = "User 1 Putting",                      NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-04"), ModifiedDate = null },
				new Activity { Name = "User 1 Reading book",                 NoOfMinutes = 30, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Mental").Id,    CreatedDate = DateTime.Parse("2019-09-05"), ModifiedDate = null },
				new Activity { Name = "User 1 Exercise",                     NoOfMinutes = 15, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Physical").Id,  CreatedDate = DateTime.Parse("2019-09-06"), ModifiedDate = null },
				new Activity { Name = "User 1 Driving",                      NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-07"), ModifiedDate = null },
				new Activity { Name = "User 1 Chipping",                     NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-08"), ModifiedDate = null },
				new Activity { Name = "User 1 Pitching",                     NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-09"), ModifiedDate = null },
				new Activity { Name = "User 1 Drinking smooties",            NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Nutrient").Id,  CreatedDate = DateTime.Parse("2019-09-10"), ModifiedDate = null },
				new Activity { Name = "User 1 Hitting on the net",           NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-11"), ModifiedDate = null },
				new Activity { Name = "User 1 Attending a technique lesson", NoOfMinutes = 45, UserId = user1.Id, CategoryId = categories.Find(x => x.Name == "User 1 Technique").Id, CreatedDate = DateTime.Parse("2019-09-12"), ModifiedDate = null },

				new Activity { Name = "User 2 Speedstick",                   NoOfMinutes = 15, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Physical").Id,  CreatedDate = DateTime.Parse("2019-09-01"), ModifiedDate = null },
				new Activity { Name = "User 2 Swing Fix",                    NoOfMinutes = 60, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-02"), ModifiedDate = null },
				new Activity { Name = "User 2 Wedge Gapping",                NoOfMinutes = 30, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-03"), ModifiedDate = null },
				new Activity { Name = "User 2 utting",                       NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-04"), ModifiedDate = null },
				new Activity { Name = "User 2 Reading book",                 NoOfMinutes = 30, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Mental").Id,    CreatedDate = DateTime.Parse("2019-09-05"), ModifiedDate = null },
				new Activity { Name = "User 2 Exercise",                     NoOfMinutes = 15, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Physical").Id,  CreatedDate = DateTime.Parse("2019-09-06"), ModifiedDate = null },
				new Activity { Name = "User 2 Driving",                      NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-07"), ModifiedDate = null },
				new Activity { Name = "User 2 Chipping",                     NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-08"), ModifiedDate = null },
				new Activity { Name = "User 2 Pitching",                     NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-09"), ModifiedDate = null },
				new Activity { Name = "User 2 Drinking smooties",            NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Nutrient").Id,  CreatedDate = DateTime.Parse("2019-09-10"), ModifiedDate = null },
				new Activity { Name = "User 2 Hitting on the net",           NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-11"), ModifiedDate = null },
				new Activity { Name = "User 2 Attending a technique lesson", NoOfMinutes = 45, UserId = user2.Id, CategoryId = categories.Find(x => x.Name == "User 2 Technique").Id, CreatedDate = DateTime.Parse("2019-09-12"), ModifiedDate = null },

			};
			activities.ForEach(s => context.Activities.AddOrUpdate(p => p.Name, s));
			context.SaveChanges();

			var messages = new List<Message>
			{
				new Message { SenderUserId = user3.Id, ReceiverUserId = user1.Id, Content = "Good Luck Arki !!!", CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Message { SenderUserId = user4.Id, ReceiverUserId = user1.Id, Content = "Good Luck Arki and stay cool", CreatedDate = DateTime.Parse("2019-09-01") , ModifiedDate = null },
				new Message { SenderUserId = user3.Id, ReceiverUserId = user1.Id, Content = "Good Luck Aaron !", CreatedDate = DateTime.Parse("2019-09-03") , ModifiedDate = null },
			};
			messages.ForEach(s => context.Messages.AddOrUpdate(p => p.Content, s));
			context.SaveChanges();

		}
	}
}
