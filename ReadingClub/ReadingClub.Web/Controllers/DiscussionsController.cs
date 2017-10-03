using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AutoMapper;

using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Web.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IMapper mapper;

        public DiscussionsController(IDiscussionsService discussionsService, IMapper mapper)
        {
            this.discussionsService = discussionsService;
            this.mapper = mapper;
        }

        // GET: Discussions
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateDiscussion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDiscussion(CreateDiscussionViewModel discussion)
        {
            //change url
            return RedirectToAction("Index", "Home");
        }
    }
}