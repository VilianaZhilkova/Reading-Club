using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity.Owin;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Areas.Administration.ViewModels.Home;

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
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(discussionsService, nameof(discussionsService)).IsNull().Throw();
            Guard.WhenArgument(booksService, nameof(booksService)).IsNull().Throw();
            Guard.WhenArgument(commentsService, nameof(commentsService)).IsNull().Throw();
            Guard.WhenArgument(authorService, nameof(authorService)).IsNull().Throw();
            Guard.WhenArgument(userManager, nameof(userManager)).IsNull().Throw();
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
        public async Task<ActionResult> Index(AdminHomeViewModel model)
        {
            Guard.WhenArgument(model, nameof(model)).IsNull().Throw();
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

            model.ApprovedDiscussionsCount = apporvedDiscussions.Count();
            model.DiscussionsForApprovalCount = discussionsForApproval.Count();
            model.ApprovedBooksCount = approvedBooks.Count();
            model.BooksForApprovalCount = booksForApproval.Count();
            model.AuthorsCount = authors.Count();
            model.CommentsCount = comments.Count();
            model.AdministratorsCount = administrators.Count();
            model.UsersCount = users.Count();

            return this.View(model);
        }
    }
}