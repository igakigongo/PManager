using System.Collections.Generic;
using PManager.Domain.Entities;

namespace PManager.Domain.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<PManager.Domain.Concrete.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(PManager.Domain.Concrete.EFDbContext context)
        {
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
