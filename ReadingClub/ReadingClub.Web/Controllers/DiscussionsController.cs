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

namespace ReadingClub.Web.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IUsersService usersService;
        private readonly IBooksService booksService;
        private readonly IDiscussionUsersData discussionUsersData;
        private readonly IMapper mapper;

        public DiscussionsController(IDiscussionsService discussionsService, IUsersService usersService, IBooksService booksService, IDiscussionUsersData discussionUsersData, IMapper mapper)
        {
            this.discussionsService = discussionsService;
            this.usersService = usersService;
            this.booksService = booksService;
            this.discussionUsersData = discussionUsersData;
            this.mapper = mapper;
        }

        // GET: Discussions
        public ActionResult Index(string discussionStatus)
        {
            if(discussionStatus == null)
            {
                return RedirectToAction("Index", "Home");
            }      

            return View();
        }

        [OutputCache(Duration = 60, VaryByParam = "discussionStatus")]
        [ChildActionOnly]
        public ActionResult AllDiscussionsPartial(string discussionStatus)
        {
            var currentDate = DateTime.UtcNow;
            var discussions = new List<DiscussionViewModel>();
            if (discussionStatus == Common.Constants.DiscussionStatusUpcoming)
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .To<DiscussionViewModel>()
                    .Where(d => d.StartDate > currentDate)
                    .OrderBy(d => d.StartDate)
                    .ThenBy(d => d.EndDate)
                    .ToList();
            }
            else if (discussionStatus == Common.Constants.DiscussionStatusCurrent)
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                     .To<DiscussionViewModel>()
                     .Where(d => d.StartDate <= currentDate && currentDate <= d.EndDate)
                     .OrderBy(d => d.StartDate)
                     .ThenBy(d => d.EndDate)
                     .ToList();
            }
            else
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .To<DiscussionViewModel>()
                    .Where(d => d.EndDate < currentDate)
                    .OrderByDescending(d => d.StartDate)
                    .ThenByDescending(d => d.EndDate)
                    .ToList();
            }
            return this.PartialView(discussions);
        }

        [HttpGet]
        public ActionResult GetById(int? discussionId)
        {
            this.discussionUsersData.GetData();
            if (discussionId == null)
            {
                return RedirectToAction("Index", new { discussionStatus = Common.Constants.DiscussionStatusUpcoming });
            }

            var discussion = this.discussionsService.GetById((int)discussionId);
            discussion.Comments = discussion.Comments.Where(x => x.IsDeleted == false).OrderBy(x => x.Date).ToList();
            var model = this.mapper.Map<DetailDiscussionViewModel>(discussion);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateDiscussion(int bookId, string bookTitle)
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [HttpPost]
        [Authorize]
        public PartialViewResult Join(int id)
        {
            this.discussionUsersData.GetData();
            var discussion = this.discussionsService.GetById(id);

            if (discussion.Users.Count() >= discussion.MaximumNumberOfParticipants)
            {
                return PartialView("_ButtonsPartial", discussion);
            }
            else
            {
                var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
                var currentUser = this.usersService.GetUserByUserName(currentUserUserName);
                  
                this.discussionsService.AddUserToDiscussion(discussion, currentUser);
 
                var model = mapper.Map<DetailDiscussionViewModel>(discussion);

                return PartialView("_ButtonsPartial", model);
            }
            
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult Leave(int id)
        {
            this.discussionUsersData.GetData();
            var discussion = this.discussionsService.GetById(id);
            var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
            var currentUser = this.usersService.GetUserByUserName(currentUserUserName);

            this.discussionsService.RemoveUserFromDiscussion(discussion, currentUser);

            var model = mapper.Map<DetailDiscussionViewModel>(discussion);

            return PartialView("_ButtonsPartial", model);
        }
    }
}