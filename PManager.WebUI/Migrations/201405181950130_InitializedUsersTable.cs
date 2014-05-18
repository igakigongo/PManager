namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializedUsersTable : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("dbo.ProjectTasks", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.ProjectTasks", "UserId");
            AddForeignKey("dbo.ProjectTasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTasks", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Id", "dbo.UserProfile");
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.ProjectTasks", new[] { "UserId" });
            DropColumn("dbo.ProjectTasks", "UserId");
            DropTable("dbo.UserProfile");
            DropTable("dbo.Users");
        }
    }
}
