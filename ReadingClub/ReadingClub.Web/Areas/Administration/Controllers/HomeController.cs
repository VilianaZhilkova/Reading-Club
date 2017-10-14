using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Home;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IDiscussionsService discussionsService;
        private readonly IBooksService booksService;
        private readonly ICommentsService commentsService;
        private readonly IAuthorsService authorService;
        private ApplicationUserManager userManager;

        public HomeController(IUsersService usersService, IDiscussionsService discussionsService, IBooksService booksService, ICommentsService commentsService, IAuthorsService authorService, ApplicationUserManager userManager)
        {
            this.usersService = usersService;
            this.discussionsService = discussionsService;
            this.booksService = booksService;
            this.commentsService = commentsService;
            this.authorService = authorService;
            this.userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }
        // GET: Administration/Home
        public async Task<ActionResult> Index()
        {
            var apporvedDiscussions = this.discussionsService.GetAllApprovedDiscussions();
            var discussionsForApproval = this.discussionsService.GetAllDiscussionsForApproval();
            var approvedBooks = this.booksService.GetAllApprovedBooks();
            var booksForApproval = this.booksService.GetAllBooksForApproval();
            var authors = this.authorService.GetAuthorsWithDeleted();
            var comments = this.commentsService.GetAllComments();

            var allUsers = this.usersService.GetAllUsers()
                .OrderBy(d => d.UserName)
                .ToList();

            var users = new List<User>();
            var administrators = new List<User>();

            foreach (var user in allUsers)
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, "User"))
                {
                    users.Add(user);
                }
                else
                {
                    administrators.Add(user);
                }
            }

            var model = new AdminHomeViewModel
            {
                ApprovedDiscussionsCount = apporvedDiscussions.Count(),
                DiscussionsForApprovalCount = discussionsForApproval.Count(),
                ApprovedBooksCount = approvedBooks.Count(),
                BooksForApprovalCount = booksForApproval.Count(),
                AuthorsCount = authors.Count(),
                CommentsCount = comments.Count(),
                AdministratorsCount = administrators.Count(),
                UsersCount = users.Count(),
            };

            return View(model);
        }
    }
}