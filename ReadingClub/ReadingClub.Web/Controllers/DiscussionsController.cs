using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using Microsoft.AspNet.Identity;

using ReadingClub.Common.Constants;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Hubs.Data;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.ViewModels.Discussions;

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
            Guard.WhenArgument(discussionsService, nameof(discussionsService)).IsNull().Throw();
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(booksService, nameof(booksService)).IsNull().Throw();
            Guard.WhenArgument(discussionUsersData, nameof(discussionUsersData)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();

            this.discussionsService = discussionsService;
            this.usersService = usersService;
            this.booksService = booksService;
            this.discussionUsersData = discussionUsersData;
            this.mapper = mapper;
        }

        // GET: Discussions
        public ActionResult Index(string discussionStatus)
        {
            if (discussionStatus == null)
            {
                return this.RedirectToAction("Index", "Home");
            }      

            return this.View();
        }

        [OutputCache(Duration = 60, VaryByParam = "discussionStatus")]
        [ChildActionOnly]
        public ActionResult AllDiscussionsPartial(string discussionStatus)
        {
            var currentDate = DateTime.UtcNow;
            var discussions = new List<DiscussionViewModel>();
            if (discussionStatus == TextConstants.DiscussionStatusUpcoming)
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .To<DiscussionViewModel>()
                    .Where(d => d.StartDate > currentDate)
                    .OrderBy(d => d.StartDate)
                    .ThenBy(d => d.EndDate)
                    .ToList();
            }
            else if (discussionStatus == TextConstants.DiscussionStatusCurrent)
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                     .To<DiscussionViewModel>()
                     .Where(d => d.StartDate <= currentDate && currentDate <= d.EndDate)
                     .OrderBy(d => d.StartDate)
                     .ThenBy(d => d.EndDate)
                     .ToList();
            }
            else if (discussionStatus == TextConstants.DiscussionStatusPassed)
            {
                discussions = this.discussionsService.GetAllApprovedDiscussions()
                    .To<DiscussionViewModel>()
                    .Where(d => d.EndDate < currentDate)
                    .OrderByDescending(d => d.StartDate)
                    .ThenByDescending(d => d.EndDate)
                    .ToList();
            }
            else
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.PartialView(discussions);
        }

        [HttpGet]
        public ActionResult GetById(int? discussionId)
        {
            if (discussionId == null)
            {
                return this.RedirectToAction("Index", new { discussionStatus = TextConstants.DiscussionStatusUpcoming });
            }

            this.discussionUsersData.GetData();

            var discussion = this.discussionsService.GetById((int)discussionId);
            discussion.Comments = discussion.Comments.Where(x => x.IsDeleted == false).OrderBy(x => x.Date).ToList();
            var model = this.mapper.Map<DetailDiscussionViewModel>(discussion);
            return this.View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult CreateDiscussion(int? bookId, string bookTitle)
        {
            if (bookId == null || bookTitle == null)
            {
                return this.RedirectToAction("Index", "Books");
            }
            return this.View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussion(CreateDiscussionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var discussion = this.mapper.Map<Discussion>(model);

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

                return this.RedirectToAction("GetById", "Discussions", new { discussionId = discussion.Id });
            }

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult Join(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();

            this.discussionUsersData.GetData();
            var discussion = this.discussionsService.GetById(id);

            if (discussion.Users.Count() >= discussion.MaximumNumberOfParticipants)
            {
                return this.PartialView("_ButtonsPartial", discussion);
            }
            else
            {
                var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
                var currentUser = this.usersService.GetUserByUserName(currentUserUserName);
                  
                this.discussionsService.AddUserToDiscussion(discussion, currentUser);
 
                var model = this.mapper.Map<DetailDiscussionViewModel>(discussion);

                return this.PartialView("_ButtonsPartial", model);
            }            
        }

        [HttpPost]
        [Authorize]
        public PartialViewResult Leave(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();

            this.discussionUsersData.GetData();
            var discussion = this.discussionsService.GetById(id);
            var currentUserUserName = System.Web.HttpContext.Current.User.Identity.GetUserName();
            var currentUser = this.usersService.GetUserByUserName(currentUserUserName);

            this.discussionsService.RemoveUserFromDiscussion(discussion, currentUser);

            var model = this.mapper.Map<DetailDiscussionViewModel>(discussion);

            return this.PartialView("_ButtonsPartial", model);
        }
    }
}