using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Areas.Administration.ViewModels.Comments;
using ReadingClub.Web.Infrastructure.Mapping;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IMapper mapper;

        public CommentsController(ICommentsService commentsService, IMapper mapper)
        {
            Guard.WhenArgument(commentsService, nameof(commentsService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            this.commentsService = commentsService;
            this.mapper = mapper;
        }

        // GET: Administration/Comments
        public ActionResult Index()
        {
            return this.RedirectToAction("CommentsOnSite");
        }

        [HttpGet]
        public ActionResult CommentsOnSite()
        {
            var commentsOnSite = this.commentsService.GetAllComments()
                .To<AdminCommentViewModel>()
                .OrderBy(d => d.Date)
                .ToList();
            return this.View(commentsOnSite);
        }

        [HttpGet]
        public ActionResult DeletedComments()
        {
            var deletedComments = this.commentsService.GetAllCommentsWithDeleted()
                .Where(x => x.IsDeleted)
                .To<AdminCommentViewModel>()
                .OrderBy(d => d.Date)
                .ToList();
            return this.View(deletedComments);
        }

        public ActionResult DeleteComment(string commentId)
        {
            if (commentId == null)
            {
                return this.RedirectToAction("CommentsOnSite");
            }

            var comment = this.commentsService.GetById(int.Parse(commentId));
            comment.IsDeleted = true;
            this.commentsService.Update(comment);

            return this.RedirectToAction("CommentsOnSite");
        }
    }
}