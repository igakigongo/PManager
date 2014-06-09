namespace PManager.WebUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userammended : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "EmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "PhoneContact", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "PhoneContact", c => c.String());
            AlterColumn("dbo.Users", "EmailAddress", c => c.String());
        }
    }
}
