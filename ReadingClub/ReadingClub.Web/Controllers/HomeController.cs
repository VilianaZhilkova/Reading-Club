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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}