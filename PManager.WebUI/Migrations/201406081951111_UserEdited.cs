namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserEdited : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "EmailAddress", c => c.String());
            AddColumn("dbo.Users", "PhoneContact", c => c.String());
            DropColumn("dbo.ProjectTasks", "TaskDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProjectTasks", "TaskDescription", c => c.String());
            DropColumn("dbo.Users", "PhoneContact");
            DropColumn("dbo.Users", "EmailAddress");
        }
    }
}
