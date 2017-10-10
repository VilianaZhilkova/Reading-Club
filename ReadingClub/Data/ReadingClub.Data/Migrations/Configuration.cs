using System.Data.Entity.Migrations;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReadingClub.Data.Models;
using System;

namespace ReadingClub.Data.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MsSqlDbContext>
    {
        private const string AdministratorUserName = "admin";
        private const string AdministratorEmail = "vilianazhilkova@abv.bg";
        private const string AdministratorPassword = "123456";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MsSqlDbContext context)
        {
            this.SeedUsers(context);

            base.Seed(context);
        }

        private void SeedUsers(MsSqlDbContext context)
        {
            if (!context.Roles.Any())
            {
                var adminRoleName = "Admin";
                var userRoleName = "User";

                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                var adminRole = new IdentityRole { Name = adminRoleName };
                var userRole = new IdentityRole { Name = userRoleName };
                roleManager.Create(adminRole);
                roleManager.Create(userRole);

                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorEmail,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.UtcNow
                };

                userManager.Create(user, AdministratorPassword);
                userManager.AddToRole(user.Id, adminRoleName);
            }
        }
    }
}
