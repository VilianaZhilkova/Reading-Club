using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Areas.Administration.ViewModels.Discussions;
using ReadingClub.Web.Infrastructure.Mapping;

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
            return this.RedirectToAction("DiscussionsOnSite");
        }

        [HttpGet]
        public ActionResult DiscussionsOnSite()
        {
            var discussionsOnSite = this.discussionsService.GetAllApprovedDiscussions()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.Id)
                .ToList();
            return this.View(discussionsOnSite);
        }

        [HttpGet]
        public ActionResult DeletedDiscussions()
        {
            var deletedDiscussions = this.discussionsService.GetAllDeletedDiscussions()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.StartDate)
                .ToList();
            return this.View(deletedDiscussions);
        }

        [HttpGet]
        public ActionResult DiscussionsForApproval()
        {
            var discussionsForApproval = this.discussionsService.GetAllDiscussionsForApproval()
                .To<AdminDiscussionViewModel>()
                .OrderBy(d => d.StartDate)
                .ToList();
            return this.View(discussionsForApproval);
        }

        public ActionResult RestoreDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return this.RedirectToAction("DeletedDiscussions");
            }

            var discussion = this.discussionsService.GetByIdWithDeleted(int.Parse(discussionId));
            discussion.IsDeleted = false;
            this.discussionsService.Update(discussion);

            return this.RedirectToAction("DeletedDiscussions");
        }

        public ActionResult DeleteDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return this.RedirectToAction("DiscussionsOnSite");
            }

            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsDeleted = true;
            this.discussionsService.Update(discussion);

            return this.RedirectToAction("DiscussionsOnSite");
        }

        public ActionResult DisapproveDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return this.RedirectToAction("DiscussionsOnSite");
            }

            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsApproved = false;
            this.discussionsService.Update(discussion);

            return this.RedirectToAction("DiscussionsOnSite");
        }

        public ActionResult ApproveDiscussion(string discussionId)
        {
            if (discussionId == null)
            {
                return this.RedirectToAction("DiscussionsForApproval");
            }

            var discussion = this.discussionsService.GetById(int.Parse(discussionId));
            discussion.IsApproved = true;
            this.discussionsService.Update(discussion);

            return this.RedirectToAction("DiscussionsForApproval");
        }
    }
}