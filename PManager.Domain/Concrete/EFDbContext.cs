﻿using PManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PManager.Domain.Concrete
{
    public class EFDbContext: DbContext
    {
        public EFDbContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public System.Data.Entity.DbSet<PManager.Domain.Entities.ProjectTask> ProjectTasks { get; set; }
    }
}
