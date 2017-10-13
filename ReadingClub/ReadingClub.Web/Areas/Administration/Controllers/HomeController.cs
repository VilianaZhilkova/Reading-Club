using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}