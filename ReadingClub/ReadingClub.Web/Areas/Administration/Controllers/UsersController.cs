using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Users;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using Bytes2you.Validation;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;
        private ApplicationUserManager userManager;

        public UsersController(IUsersService usersService, IMapper mapper, ApplicationUserManager userManager)
        {
            Guard.WhenArgument(usersService, nameof(usersService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            Guard.WhenArgument(userManager, nameof(userManager)).IsNull().Throw();
            this.usersService = usersService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                this.userManager = value;
            }
        }
        // GET: Administration/Users
        public ActionResult Index()
        {
            return RedirectToAction("UsersOnSite");
        }

        [HttpGet]
        public async Task<ActionResult> UsersOnSite()
        {
            var users = this.usersService.GetAllUsers()
                 .OrderBy(d => d.UserName)
                .ToList();

            var usersOnSite = new List<AdminUserViewModel>();

            foreach (var user in users)
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, "User"))
                {
                    usersOnSite.Add(this.mapper.Map<AdminUserViewModel>(user));
                }
            }
            return View(usersOnSite);
        }


        [HttpGet]
        public async Task<ActionResult> Administrators()
        {
            var users = this.usersService.GetAllUsers()
                 .OrderBy(d => d.UserName)
                .ToList();

            var administrators = new List<AdminUserViewModel>();

            foreach(var user in users)
            {
                if (await this.UserManager.IsInRoleAsync(user.Id, "Admin"))
                {
                    administrators.Add(this.mapper.Map<AdminUserViewModel>(user));
                }
            }

            return View(administrators);
        }

        public async Task<ActionResult> ChangeRoleToAdmin(string userName)
        {
            if(userName == null)
            {
                return RedirectToAction("UsersOnSite");
            }
            var user = this.usersService.GetUserByUserName(userName);
            await this.UserManager.RemoveFromRolesAsync(user.Id, "User");
            UserManager.AddToRole(user.Id, "Admin");

            return RedirectToAction("UsersOnSite");
        }

        public async Task<ActionResult> ChangeRoleToUser(string userName)
        {
            if(userName == null)
            {
                return RedirectToAction("Administrators");
            }
            var user = this.usersService.GetUserByUserName(userName);
            await this.UserManager.RemoveFromRolesAsync(user.Id, "Admin");
            UserManager.AddToRole(user.Id, "User");

            return RedirectToAction("Administrators");
        }
    }
}