namespace PManager.WebUI.Migrations
{
    using PManager.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PManager.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"Migrations";
        }

        protected override void Seed(PManager.Domain.Concrete.EFDbContext context)
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

            #region I have aligned the dates in the db: they were meant to be of Date type rather than DateTime, also DataType.Date on Estimated.StartDate and EndDate Fields
            /*
            context.Projects.Add(
                new Project
                {
                    Description = "This is a seed example to test creating a project with tasks in it",
                    Estimated =
                    {
                        Budget = 90000,
                        StartDate = new DateTime(2014, 01, 01),
                        EndDate = new DateTime(2014, 01, 30)
                    },
                    IsClosed = false,
                    Name = "Mendelevium",
                    ProjectCode = "BOU-M678",
                    ProjectTasks = new List<ProjectTask>()
                    {
                        new ProjectTask {
                            Estimated = {
                                Budget = 900,
                                EndDate = new DateTime(2014, 01, 15),
                                StartDate = new DateTime(2014, 01, 01)
                            },
                            TaskDescription = "Sample Description 2",
                            TaskName = "Creating info",
                            UserId = 1
                        },
                        new ProjectTask {
                            Estimated = {
                                Budget = 900,
                                EndDate = new DateTime(2014, 01, 30),
                                StartDate = new DateTime(2014, 01, 16)
                            },
                            TaskDescription = "Sample Desc2",
                            TaskName = "Creating info 2",
                            UserId = 1
                        }
                    }
                }
            );*/
            #endregion
            
        }
    }
}
