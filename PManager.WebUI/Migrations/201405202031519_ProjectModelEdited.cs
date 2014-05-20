namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectModelEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "IsClosed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "IsClosed");
        }
    }
}
