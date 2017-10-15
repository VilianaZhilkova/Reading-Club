using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Web.ViewModels.Home;

namespace ReadingClub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public HomeController(IDiscussionsService discussionsService, IBooksService booksService, IMapper mapper)
        {
            Guard.WhenArgument(discussionsService, nameof(discussionsService)).IsNull().Throw();
            Guard.WhenArgument(booksService, nameof(booksService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            this.discussionsService = discussionsService;
            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index(HomePageViewModel model)
        {
            Guard.WhenArgument(model, nameof(model)).IsNull().Throw();
            if (User.IsInRole("Admin"))
            {
                return this.RedirectToAction("Index", "Home", new { area = "Administration" });
            }

            var currentDate = DateTime.UtcNow;
            var currentDiscussions = this.discussionsService.GetAllApprovedDiscussions()
                     .To<DiscussionViewModel>()
                     .Where(d => d.StartDate <= currentDate && currentDate <= d.EndDate)
                     .OrderBy(d => d.StartDate)
                     .ThenBy(d => d.EndDate)
                     .Take(2)
                     .ToList();

            var upcomingDiscussions = this.discussionsService.GetAllApprovedDiscussions()
                    .To<DiscussionViewModel>()
                    .Where(d => d.StartDate > currentDate)
                    .OrderBy(d => d.StartDate)
                    .ThenBy(d => d.EndDate)
                    .Take(2)
                    .ToList();

            var books = this.booksService.GetAllApprovedBooks()
                    .OrderBy(b => b.Discussions.Count)
                    .To<BookViewModel>()
                    .Take(3)
                    .ToList();

            model.UpcomingDiscussions = upcomingDiscussions;
            model.CurrentDiscussions = currentDiscussions;
            model.Books = books;

            return this.View(model);
        }

        public ActionResult Search(string searchIn, string searchText)
        {
            if (searchIn == null || searchText == null)
            {
                return this.RedirectToAction("Index");
            }

            var model = new SearchViewModel();
            searchIn = searchIn.ToLower();
            searchText = searchText.ToLower();
            if (searchIn == "books")
            {
                var books = this.booksService.GetAllApprovedBooks()
                    .Where(x => x.Title.Contains(searchText))
                    .OrderBy(b => b.Discussions.Count)
                    .To<BookViewModel>()
                    .ToList();
                model.Books = books;
                model.Discussions = new HashSet<DiscussionViewModel>();
                return this.View(model);
            }

            var discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .Where(x => x.Book.Title.Contains(searchText))
                    .To<DiscussionViewModel>()                
                    .OrderBy(d => d.StartDate)
                    .ThenBy(d => d.EndDate)
                    .ToList();
            model.Discussions = discussions;
            model.Books = new HashSet<BookViewModel>();

            return this.View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return this.View();
        }
    }
}