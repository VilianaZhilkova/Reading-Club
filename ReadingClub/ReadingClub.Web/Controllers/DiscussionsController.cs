using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Web.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IUsersService usersService;
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public DiscussionsController(IDiscussionsService discussionsService, IUsersService usersService, IBooksService booksService, IMapper mapper)
        {
            this.discussionsService = discussionsService;
            this.usersService = usersService;
            this.booksService = booksService;
            this.mapper = mapper;
        }

        // GET: Discussions
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateDiscussion(int bookId, string bookTitle)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussion(CreateDiscussionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var discussion = mapper.Map<Discussion>(model);

                discussion.StartDate = discussion.StartDate.AddMinutes(model.TimezoneOffset);
                discussion.EndDate = discussion.EndDate.AddMinutes(model.TimezoneOffset);

                var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
                var currentUser = this.usersService.GetUserByUserName(currentUserUserName);

                discussion.Creator = currentUser;
                discussion.Users.Add(currentUser);

                var bookId = int.Parse(Request.Params["bookId"]);
                var book = this.booksService.GetById(bookId);
                discussion.Book = book;
                this.discussionsService.AddDiscussion(discussion);
                //change url
                return RedirectToAction("Index", "Home");
            }

            return View(model);

        }
    }
}