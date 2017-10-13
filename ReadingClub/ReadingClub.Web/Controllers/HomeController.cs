using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Web.Hubs.Data;
using ReadingClub.Web.ViewModels.Home;
using ReadingClub.Web.ViewModels.Books;

namespace ReadingClub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public HomeController(IDiscussionsService discussionsService, IBooksService booksService, IMapper mapper)
        {
            this.discussionsService = discussionsService;
            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index(HomePageViewModel model)
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { area = "Administration" });
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

            return View(model);
        }

        public ActionResult Search(string searchIn, string searchText)
        {
            if(searchIn == null || searchText == null)
            {
                return RedirectToAction("Index");
            }
            var model = new SearchViewModel();
            searchIn = searchIn.ToLower();
            searchText = searchText.ToLower();
            if(searchIn == "books")
            {
                var books = this.booksService.GetAllApprovedBooks()
                    .Where(x => x.Title.Contains(searchText))
                    .OrderBy(b => b.Discussions.Count)
                    .To<BookViewModel>()
                    .ToList();
                model.Books = books;
                model.Discussions = new HashSet<DiscussionViewModel>();
                return View(model);
            }

            var discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .Where(x => x.Book.Title.Contains(searchText))
                    .To<DiscussionViewModel>()                
                    .OrderBy(d => d.StartDate)
                    .ThenBy(d => d.EndDate)
                    .ToList();
            model.Discussions = discussions;
            model.Books = new HashSet<BookViewModel>();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}