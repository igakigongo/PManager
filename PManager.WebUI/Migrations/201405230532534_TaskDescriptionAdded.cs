namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskDescriptionAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProjectTasks", "TaskDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProjectTasks", "TaskDescription");
        }
    }
}
