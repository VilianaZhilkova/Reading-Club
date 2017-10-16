using System;
using System.Data.Entity.Migrations;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ReadingClub.Data.Models;
using System.Collections.Generic;

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
            if(!context.Authors.Any())
            {
                this.SeedSampleData(context);
            }
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
                var admin = new User
                {
                    UserName = AdministratorUserName,
                    Email = AdministratorEmail,
                    EmailConfirmed = true,
                    CreatedOn = DateTime.UtcNow
                };

                var usersUserNames = new string[5] { "petar", "georgi", "viliana", "mihaela", "daniela" };
                var usersEmail = new string[5] { "petar@abv.bg", "georgi@abv.bg", "viliana@abv.bg", "mihaela@abv.bg", "daniela@abv.bg" };

                for(var i = 0; i < usersUserNames.Length; i++)
                {
                    var user = new User
                    {
                        UserName = usersUserNames[i],
                        Email = usersEmail[i],
                        EmailConfirmed = true,
                        CreatedOn = DateTime.UtcNow
                    };

                    userManager.Create(user, AdministratorPassword);
                    userManager.AddToRole(user.Id, "User");
                }

                userManager.Create(admin, AdministratorPassword);
                userManager.AddToRole(admin.Id, adminRoleName);
            }
        }

        private void SeedSampleData(MsSqlDbContext context)
        {
            //Authors
            var authorsNames = new string[3] { "F. Scott Fitzgerald", "Harper Lee", "George R. R. Martin" };

            //Books
            var bookTitles = new string[3] { "The Great Gatsby", "To Kill a Mockingbird", "A Game of Thrones" };

            var firstDescription = "The Great Gatsby is a 1925 novel written by American author F. Scott Fitzgerald";
            var secondDescription = "To Kill a Mockingbird is a novel by Harper Lee published in 1960";
            var thirdDescription = "A Game of Thrones is the first novel in A Song of Ice and Fire, a series of fantasy novels";

            var booksDescriptions = new string[3] { firstDescription, secondDescription, thirdDescription};

            //Discussions

            var discussionsSubjects = new string[3] { "The Great Gatsby Discussion", "To Kill a Mockingbird Discussion", "A Game of Thrones Discussion" };
            var startDates = new DateTime[3] { DateTime.UtcNow.AddDays(-2), DateTime.UtcNow, DateTime.UtcNow.AddDays(2) };
            var endDates = new DateTime[3] { DateTime.UtcNow.AddDays(-2).AddHours(2), DateTime.UtcNow.AddHours(4), DateTime.UtcNow.AddDays(2).AddHours(3) };
            var maxParticipants = new int[3] { 5, 4, 10 };

            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var usersUserNames = new string[5] { "petar", "georgi", "viliana", "mihaela", "daniela" };

            for (var i = 0; i < authorsNames.Length; i++)
            {
                //user 
                var user = userManager.Users.ToList().First(x => x.UserName == usersUserNames[i]);
                //Author
                var author = new Author
                {
                    Name = authorsNames[i]
                };

                context.Authors.Add(author);
                context.SaveChanges();
                //Book
                var book = new Book
                {
                    Title = bookTitles[i], 
                    Author = author,
                    Description = booksDescriptions[i],
                    IsApproved = true
                };

                context.Books.Add(book);
                context.SaveChanges();
                //Discussions

                var discussion = new Discussion
                {
                    Subject = discussionsSubjects[i],
                    StartDate = startDates[i],
                    EndDate = endDates[i],
                    Creator = user,
                    Book = book,
                    IsApproved = true,
                    MaximumNumberOfParticipants = maxParticipants[i],
                    Users = new HashSet<User>()
                };
                discussion.Users.Add(user);
                discussion.Users.Add(userManager.Users.ToList().First(x => x.UserName == usersUserNames[i + 1]));
                context.Discussions.Add(discussion);
                context.SaveChanges();
                if (i == 2)
                {
                    for(var j = 0; j < 3; j++)
                    {
                        var comment = new Comment
                        {
                            Author = user,
                            Content = "Commment content" + j,
                            Date = DateTime.UtcNow.AddDays(-2).AddMinutes(10),
                            Discussion = discussion
                        };
                        context.Comments.Add(comment);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
