namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
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
                        TaskName = c.String(nullable: false),
                        Actual_StartDate = c.DateTime(nullable: false),
                        Actual_EndDate = c.DateTime(nullable: false),
                        Actual_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estimated_StartDate = c.DateTime(nullable: false),
                        Estimated_EndDate = c.DateTime(nullable: false),
                        Estimated_Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectTasks", new[] { "ProjectId" });
            DropTable("dbo.ProjectTasks");
            DropTable("dbo.Projects");
        }
    }
}
