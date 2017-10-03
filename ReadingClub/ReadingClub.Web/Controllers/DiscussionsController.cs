using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Web.Controllers
{
    public class DiscussionsController : Controller
    {
        private readonly IDiscussionsService discussionsService;
        private readonly IMapperProvider mapper;

        public DiscussionsController(IDiscussionsService discussionsService, IMapperProvider mapper)
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