using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadingClub.Web.Controllers
{
    public class DiscussionsController : Controller
    {
        // GET: Discussions
        public ActionResult Index()
        {
            return View();
        }
    }
}