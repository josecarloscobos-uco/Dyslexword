namespace Dyslexword.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlumnoTutors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserAlumnoId = c.String(),
                        UserTutorId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 60),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        OrderWordId = c.Int(nullable: false),
                        OrderPhraseId = c.Int(nullable: false),
                        GuessId = c.Int(nullable: false),
                        LongReadingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guesses", t => t.GuessId)
                .ForeignKey("dbo.LongReadings", t => t.LongReadingId)
                .ForeignKey("dbo.OrderPhrases", t => t.OrderPhraseId)
                .ForeignKey("dbo.OrderWords", t => t.OrderWordId)
                .Index(t => t.OrderWordId)
                .Index(t => t.OrderPhraseId)
                .Index(t => t.GuessId)
                .Index(t => t.LongReadingId);
            
            CreateTable(
                "dbo.LongReadings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 60),
                        Text = c.String(nullable: false, maxLength: 255),
                        Question = c.String(nullable: false, maxLength: 60),
                        CorrectOption = c.String(nullable: false, maxLength: 60),
                        OptionA = c.String(nullable: false, maxLength: 60),
                        OptionB = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderPhrases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phrase = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 60),
                        Image = c.Binary(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 40),
                        LastName = c.String(nullable: false, maxLength: 40),
                        Coins = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Birthdate = c.DateTime(nullable: false),
                        Login = c.DateTime(nullable: false),
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Levels", "OrderWordId", "dbo.OrderWords");
            DropForeignKey("dbo.Levels", "OrderPhraseId", "dbo.OrderPhrases");
            DropForeignKey("dbo.Levels", "LongReadingId", "dbo.LongReadings");
            DropForeignKey("dbo.Levels", "GuessId", "dbo.Guesses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Levels", new[] { "LongReadingId" });
            DropIndex("dbo.Levels", new[] { "GuessId" });
            DropIndex("dbo.Levels", new[] { "OrderPhraseId" });
            DropIndex("dbo.Levels", new[] { "OrderWordId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderWords");
            DropTable("dbo.OrderPhrases");
            DropTable("dbo.LongReadings");
            DropTable("dbo.Levels");
            DropTable("dbo.Guesses");
            DropTable("dbo.AlumnoTutors");
        }
    }
}
