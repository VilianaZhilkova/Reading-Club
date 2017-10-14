using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Discussions;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using Bytes2you.Validation;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IMapper mapper;

        public DiscussionsController(IDiscussionsService discussionsService, IMapper mapper)
        {
            Guard.WhenArgument(discussionsService, nameof(discussionsService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            this.discussionsService = discussionsService;
            this.mapper = mapper;
        }
        // GET: Administration/Discussions
        public ActionResult Index()
        {
            return RedirectToAction("DiscussionsOnSite");
        }

        [HttpGet]
        public ActionResult DiscussionsOnSite()
        {
            var discussionsOnSite = this.discussionsService.GetAllApprovedDiscussions()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.Id)
                .ToList();
            return View(discussionsOnSite);
        }

        [HttpGet]
        public ActionResult DeletedDiscussions()
        {
            var deletedDiscussions = this.discussionsService.GetAllDeletedDiscussions()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.StartDate)
                .ToList();
            return View(deletedDiscussions);
        }

        [HttpGet]
        public ActionResult DiscussionsForApproval()
        {
            var discussionsForApproval = this.discussionsService.GetAllDiscussionsForApproval()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.StartDate)
                .ToList();
            return View(discussionsForApproval);
        }

        public ActionResult RestoreDiscussion(string discussionId)
        {
            if(discussionId == null)
            {
                return RedirectToAction("DeletedDiscussions");
            }
            var discussion = this.discussionsService.GetByIdWithDeleted(int.Parse(discussionId));
            discussion.IsDeleted = false;
            this.discussionsService.Update(discussion);

            return RedirectToAction("DeletedDiscussions");
        }

        public ActionResult DeleteDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return RedirectToAction("DiscussionsOnSite");
            }
            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsDeleted = true;
            this.discussionsService.Update(discussion);

            return RedirectToAction("DiscussionsOnSite");
        }

        public ActionResult DisapproveDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return RedirectToAction("DiscussionsOnSite");
            }
            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsApproved = false;
            this.discussionsService.Update(discussion);

            return RedirectToAction("DiscussionsOnSite");
        }

        public ActionResult ApproveDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return RedirectToAction("DiscussionsForApproval");
            }
            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsApproved = true;
            this.discussionsService.Update(discussion);

            return RedirectToAction("DiscussionsForApproval");
        }
    }
}