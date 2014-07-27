namespace PManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Laptops",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            SerialNumber = c.String(nullable: false),
            //            LaptopName = c.String(nullable: false),
            //            Cost = c.Double(nullable: false),
            //            Project_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Projects", t => t.Project_Id)
            //    .Index(t => t.Project_Id);
            
            //CreateTable(
            //    "dbo.Projects",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ProjectCode = c.String(nullable: false),
            //            Name = c.String(nullable: false),
            //            IsClosed = c.Boolean(nullable: false),
            //            Actual_StartDate = c.DateTime(storeType: "date"),
            //            Actual_EndDate = c.DateTime(storeType: "date"),
            //            Actual_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Estimated_StartDate = c.DateTime(nullable: false, storeType: "date"),
            //            Estimated_EndDate = c.DateTime(nullable: false, storeType: "date"),
            //            Estimated_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Description = c.String(),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.ProjectTasks",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            ProjectId = c.Int(nullable: false),
            //            TaskName = c.String(nullable: false),
            //            IsCompleted = c.Boolean(nullable: false),
            //            Actual_StartDate = c.DateTime(storeType: "date"),
            //            Actual_EndDate = c.DateTime(storeType: "date"),
            //            Actual_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            Estimated_StartDate = c.DateTime(nullable: false, storeType: "date"),
            //            Estimated_EndDate = c.DateTime(nullable: false, storeType: "date"),
            //            Estimated_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
            //            TeamId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
            //    .ForeignKey("dbo.Teams", t => t.TeamId)
            //    .Index(t => t.ProjectId)
            //    .Index(t => t.TeamId);
            
            //CreateTable(
            //    "dbo.Teams",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            Name = c.String(),
            //            Status = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false),
            //            Firstname = c.String(nullable: false),
            //            Middlename = c.String(),
            //            Lastname = c.String(nullable: false),
            //            EmailAddress = c.String(nullable: false),
            //            PhoneContact = c.String(nullable: false),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.UserProfile", t => t.Id)
            //    .Index(t => t.Id);
            
            //CreateTable(
            //    "dbo.UserProfile",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            UserName = c.String(),
            //        })
            //    .PrimaryKey(t => t.UserId);
            
            //CreateTable(
            //    "dbo.Vehicles",
            //    c => new
            //        {
            //            Id = c.Int(nullable: false, identity: true),
            //            NumberPlate = c.String(nullable: false),
            //            CarType = c.String(nullable: false),
            //            Project_Id = c.Int(),
            //        })
            //    .PrimaryKey(t => t.Id)
            //    .ForeignKey("dbo.Projects", t => t.Project_Id)
            //    .Index(t => t.Project_Id);
            
            //CreateTable(
            //    "dbo.UserTeams",
            //    c => new
            //        {
            //            User_Id = c.Int(nullable: false),
            //            Team_Id = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.User_Id, t.Team_Id })
            //    .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
            //    .ForeignKey("dbo.Teams", t => t.Team_Id, cascadeDelete: true)
            //    .Index(t => t.User_Id)
            //    .Index(t => t.Team_Id);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Vehicles", "Project_Id", "dbo.Projects");
            //DropForeignKey("dbo.ProjectTasks", "TeamId", "dbo.Teams");
            //DropForeignKey("dbo.Users", "Id", "dbo.UserProfile");
            //DropForeignKey("dbo.UserTeams", "Team_Id", "dbo.Teams");
            //DropForeignKey("dbo.UserTeams", "User_Id", "dbo.Users");
            //DropForeignKey("dbo.ProjectTasks", "ProjectId", "dbo.Projects");
            //DropForeignKey("dbo.Laptops", "Project_Id", "dbo.Projects");
            //DropIndex("dbo.UserTeams", new[] { "Team_Id" });
            //DropIndex("dbo.UserTeams", new[] { "User_Id" });
            //DropIndex("dbo.Vehicles", new[] { "Project_Id" });
            //DropIndex("dbo.Users", new[] { "Id" });
            //DropIndex("dbo.ProjectTasks", new[] { "TeamId" });
            //DropIndex("dbo.ProjectTasks", new[] { "ProjectId" });
            //DropIndex("dbo.Laptops", new[] { "Project_Id" });
            //DropTable("dbo.UserTeams");
            //DropTable("dbo.Vehicles");
            //DropTable("dbo.UserProfile");
            //DropTable("dbo.Users");
            //DropTable("dbo.Teams");
            //DropTable("dbo.ProjectTasks");
            //DropTable("dbo.Projects");
            //DropTable("dbo.Laptops");
        }
    }
}
