using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Comments;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using Bytes2you.Validation;

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
            return RedirectToAction("CommentsOnSite");
        }

        [HttpGet]
        public ActionResult CommentsOnSite()
        {
            var commentsOnSite = this.commentsService.GetAllComments()
                .To<AdminCommentViewModel>()
                .OrderBy(d => d.Date)
                .ToList();
            return View(commentsOnSite);
        }

        [HttpGet]
        public ActionResult DeletedComments()
        {
            var deletedComments = this.commentsService.GetAllCommentsWithDeleted()
                .Where(x => x.IsDeleted)
                .To<AdminCommentViewModel>()
                .OrderBy(d => d.Date)
                .ToList();
            return View(deletedComments);
        }

        public ActionResult DeleteComment(string commentId)
        {
            if(commentId == null)
            {
                return RedirectToAction("CommentsOnSite");
            }
            var comment = this.commentsService.GetById(int.Parse(commentId));
            comment.IsDeleted = true;
            this.commentsService.Update(comment);

            return RedirectToAction("CommentsOnSite");
        }

    }
}