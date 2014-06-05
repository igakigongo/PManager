namespace PManager.WebUI.Migrations
{
    using PManager.Domain.Concrete;
    using PManager.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(EFDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var projects = new List<Project>
            {
                new Project{
                    Description = "Migrating Server Resources to great lakes region in Uganda",
                    Estimated = new Estimated{
                        Budget = new Decimal(12000),
                        EndDate = new DateTime(2018, 12, 31),
                        StartDate = new DateTime(2015, 01, 01)
                    },
                    IsClosed = false,
                    Name = "Krypton v1.08",
                    ProjectCode = "BOU-256"
                },
                new Project{
                    Description = "Adopting new techniques and/or strategies of calculating inflation",
                    Estimated = new Estimated{
                        Budget = new Decimal(4000),
                        EndDate = new DateTime(2021, 12, 31),
                        StartDate = new DateTime(2020, 1, 1)
                    },
                    IsClosed = false,
                    Name = "Arsenic v3.9",
                    ProjectCode = "BOU-777"
                },
                new Project{
                    Description = "Investigating and Implementing new fiscal monetary policy in Uganda",
                    Estimated = new Estimated{
                        Budget = new Decimal(7000),
                        EndDate = new DateTime(2018, 12, 31),
                        StartDate = new DateTime(2017, 1, 1)
                    },
                    IsClosed = false,
                    Name = "Titanium v2.1",
                    ProjectCode = "BOU-298"
                }
            };
            context.Projects.AddOrUpdate(projects.ToArray());
            context.SaveChanges();
        }
    }
}
