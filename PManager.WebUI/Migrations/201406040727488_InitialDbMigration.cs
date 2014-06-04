namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDbMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectCode = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        IsClosed = c.Boolean(nullable: false),
                        Actual_StartDate = c.DateTime(nullable: false),
                        Actual_EndDate = c.DateTime(nullable: false),
                        Actual_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estimated_StartDate = c.DateTime(nullable: false),
                        Estimated_EndDate = c.DateTime(nullable: false),
                        Estimated_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        TaskName = c.String(nullable: false),
                        TaskDescription = c.String(),
                        IsCompleted = c.Boolean(nullable: false),
                        Actual_StartDate = c.DateTime(nullable: false),
                        Actual_EndDate = c.DateTime(nullable: false),
                        Actual_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estimated_StartDate = c.DateTime(nullable: false),
                        Estimated_EndDate = c.DateTime(nullable: false),
                        Estimated_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Firstname = c.String(nullable: false),
                        Middlename = c.String(),
                        Lastname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Id", "dbo.UserProfile");
            DropForeignKey("dbo.ProjectTasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.ProjectTasks", new[] { "UserId" });
            DropIndex("dbo.ProjectTasks", new[] { "ProjectId" });
            DropTable("dbo.UserProfile");
            DropTable("dbo.Users");
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Projects");
        }
    }
}
