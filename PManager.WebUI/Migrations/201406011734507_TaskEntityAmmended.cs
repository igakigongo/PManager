namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskEntityAmmended : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "IsCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "IsCompleted");
        }
    }
}
