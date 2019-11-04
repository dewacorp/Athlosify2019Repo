namespace AthlosifyWebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CategoryId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(maxLength: 256),
                        NoOfMinutes = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ParentId = c.Int(),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ParentId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        NoteId = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 256),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comments", t => t.ParentId)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ParentId)
                .Index(t => t.UserId)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CategoryId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 64),
                        Content = c.String(maxLength: 1024),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        SenderUserId = c.String(maxLength: 128),
                        ReceiverUserId = c.String(maxLength: 128),
                        Content = c.String(nullable: false, maxLength: 512),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Messages", t => t.ParentId)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiverUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderUserId)
                .Index(t => t.ParentId)
                .Index(t => t.SenderUserId)
                .Index(t => t.ReceiverUserId);
            
            CreateTable(
                "dbo.NoteShares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NoteId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Notes", t => t.NoteId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.NoteId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CategoryId = c.Int(),
                        Subject = c.String(nullable: false, maxLength: 64),
                        Location = c.String(maxLength: 64),
                        Description = c.String(maxLength: 256),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        IsAllDayEvent = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.TeamMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(),
                        UserId = c.String(maxLength: 128),
                        UserType = c.String(nullable: false, maxLength: 32),
                        Description = c.String(maxLength: 256),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.TeamId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Name = c.String(maxLength: 64),
                        Description = c.String(maxLength: 256),
                        TeamPhoto = c.String(maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        FirstName = c.String(maxLength: 64),
                        LastName = c.String(maxLength: 64),
                        Gender = c.String(maxLength: 64),
                        BirthDate = c.DateTime(),
                        Suburb = c.String(maxLength: 64),
                        State = c.String(maxLength: 64),
                        Postcode = c.String(maxLength: 64),
                        Country = c.String(maxLength: 64),
                        Handicap = c.Single(),
                        ProfileDetail = c.String(maxLength: 512),
                        ClubName = c.String(maxLength: 64),
                        ProfilePhoto = c.String(maxLength: 128),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletedBy = c.String(),
                        DeletedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserProfiles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMembers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamMembers", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Schedules", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Schedules", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.NoteShares", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NoteShares", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Messages", "SenderUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ReceiverUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ParentId", "dbo.Messages");
            DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notes", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Comments", "ParentId", "dbo.Comments");
            DropForeignKey("dbo.Activities", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Activities", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "ParentId", "dbo.Categories");
            DropIndex("dbo.UserProfiles", new[] { "UserId" });
            DropIndex("dbo.Teams", new[] { "UserId" });
            DropIndex("dbo.TeamMembers", new[] { "UserId" });
            DropIndex("dbo.TeamMembers", new[] { "TeamId" });
            DropIndex("dbo.Schedules", new[] { "CategoryId" });
            DropIndex("dbo.Schedules", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.NoteShares", new[] { "UserId" });
            DropIndex("dbo.NoteShares", new[] { "NoteId" });
            DropIndex("dbo.Messages", new[] { "ReceiverUserId" });
            DropIndex("dbo.Messages", new[] { "SenderUserId" });
            DropIndex("dbo.Messages", new[] { "ParentId" });
            DropIndex("dbo.Notes", new[] { "CategoryId" });
            DropIndex("dbo.Notes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "NoteId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ParentId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Categories", new[] { "ParentId" });
            DropIndex("dbo.Categories", new[] { "UserId" });
            DropIndex("dbo.Activities", new[] { "CategoryId" });
            DropIndex("dbo.Activities", new[] { "UserId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Teams");
            DropTable("dbo.TeamMembers");
            DropTable("dbo.Schedules");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.NoteShares");
            DropTable("dbo.Messages");
            DropTable("dbo.Notes");
            DropTable("dbo.Comments");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.Activities");
        }
    }
}
