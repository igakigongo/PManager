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
            ///  This method will be called after migrating to the latest version.
            context.Users.AddOrUpdate(
                _user => _user.Id,
                new User
                {
                    EmailAddress = "robert.van.der.warf@moonrise.hk",
                    //EmailAddress = "oneway@redmond.com",
                    Firstname = "Warf",
                    Id = 1,
                    Lastname = "Robert",
                    Middlename = "Van Der",
                    PhoneContact = "00085265060294"
                }
            );

            #region Projects
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
            context.Projects.AddOrUpdate(p => p.ProjectCode, projects.ToArray());
            context.SaveChanges();
            #endregion

            #region Project Tasks
            var projecttasks = new List<ProjectTask>
            {
                new ProjectTask{
                    Estimated = new Estimated{
                        Budget = new Decimal(900),
                        EndDate = new DateTime(2018, 12, 31),
                        StartDate = new DateTime(2018, 10, 01)
                    },
                    IsCompleted = false,
                    ProjectId = 2,
                    TaskName = "Summarizing Project Artifacts",
                }
            };
            context.ProjectTasks.AddOrUpdate(t => t.TaskName, projecttasks.ToArray());
            context.SaveChanges();
            #endregion

        }
    }
}  
