using System.Collections.Generic;
using PManager.Domain.Entities;

namespace PManager.Domain.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PManager.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PManager.Domain.Concrete.EFDbContext context)
        {
            //#region Projects
            //var projects = new List<Project>
            //{
            //    new Project{
            //        Description = "Migrating Server Resources to great lakes region in Uganda",
            //        Estimated = new Estimated{
            //            Budget = new Decimal(12000),
            //            EndDate = new DateTime(2018, 12, 31),
            //            StartDate = new DateTime(2015, 01, 01)
            //        },
            //        IsClosed = false,
            //        Name = "Krypton v1.08",
            //        ProjectCode = "BOU-256"
            //    },
            //    new Project{
            //        Description = "Adopting new techniques and/or strategies of calculating inflation",
            //        Estimated = new Estimated{
            //            Budget = new Decimal(4000),
            //            EndDate = new DateTime(2021, 12, 31),
            //            StartDate = new DateTime(2020, 1, 1)
            //        },
            //        IsClosed = false,
            //        Name = "Arsenic v3.9",
            //        ProjectCode = "BOU-777"
            //    },
            //    new Project{
            //        Description = "Investigating and Implementing new fiscal monetary policy in Uganda",
            //        Estimated = new Estimated{
            //            Budget = new Decimal(7000),
            //            EndDate = new DateTime(2018, 12, 31),
            //            StartDate = new DateTime(2017, 1, 1)
            //        },
            //        IsClosed = false,
            //        Name = "Titanium v2.1",
            //        ProjectCode = "BOU-298"
            //    }
            //};
            //context.Projects.AddOrUpdate(p => p.ProjectCode, projects.ToArray());
            //context.SaveChanges();
            //#endregion

            //#region Project Tasks
            //var projecttasks = new List<ProjectTask>
            //{
            //    new ProjectTask{
            //        Estimated = new Estimated{
            //           Budget = new Decimal(900),
            //           EndDate = new DateTime(2018, 12, 31),
            //           StartDate = new DateTime(2018, 10, 01)
            //        },
            //        IsCompleted = false,
            //        ProjectId = 1,
            //        TaskName = "Summarizing Project Artifacts"
            //    },
            //    new ProjectTask{
            //        Estimated = new Estimated{
            //           Budget = new Decimal(900),
            //           EndDate = new DateTime(2018, 12, 31),
            //           StartDate = new DateTime(2018, 10, 01)
            //        },
            //        IsCompleted = false,
            //        ProjectId = 1,
            //        TaskName = "Create Schematics"
            //    },
            //    new ProjectTask{
            //        Estimated = new Estimated{
            //           Budget = new Decimal(900),
            //           EndDate = new DateTime(2018, 12, 31),
            //           StartDate = new DateTime(2018, 10, 01)
            //        },
            //        IsCompleted = false,
            //        ProjectId = 1,
            //        TaskName = "Feasibility Study"
            //    }
            //};
            //context.ProjectTasks.AddOrUpdate(t => t.TaskName, projecttasks.ToArray());
            //context.SaveChanges();
            //#endregion

            var teams = new List<Team>
            {
                new Team{
                    Id = 1,
                    Name = "BOU-453 Outsourcing Team"
                }
            };
            context.Teams.AddOrUpdate(t => t.Id, teams.ToArray());
            context.SaveChanges();

            var users = new List<User>
            {
                new User{
                    EmailAddress = "admin@erbium.org",
                    Firstname = "Herbert",
                    Id = 1,
                    Lastname = "Krypton",
                    PhoneContact = "000256776523452"
                },
                new User{
                    EmailAddress = "manager@erbium.org",
                    Firstname = "Dean",
                    Id = 2,
                    Lastname = "Edmond",
                    PhoneContact = "000256778573412"
                },
                new User{
                    EmailAddress = "lambert@erbium.org",
                    Firstname = "Lambert",
                    Id = 3,
                    Lastname = "Christopher",
                    PhoneContact = "000256776523452"
                }
            };

            context.Users.AddOrUpdate(u => u.Id, users.ToArray());
            context.SaveChanges();
            
        }
    }
}
