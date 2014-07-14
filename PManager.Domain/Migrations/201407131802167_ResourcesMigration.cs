namespace PManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourcesMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Laptops",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SerialNumber = c.String(nullable: false),
                        LaptopName = c.String(nullable: false),
                        Cost = c.Double(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberPlate = c.String(nullable: false),
                        CarType = c.String(nullable: false),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.Project_Id)
                .Index(t => t.Project_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Laptops", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Vehicles", new[] { "Project_Id" });
            DropIndex("dbo.Laptops", new[] { "Project_Id" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.Laptops");
        }
    }
}
