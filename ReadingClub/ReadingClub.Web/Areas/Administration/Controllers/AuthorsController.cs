using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Authors;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;
using Bytes2you.Validation;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService authorsService;
        private readonly IMapper mapper;

        public AuthorsController(IAuthorsService authorsService, IMapper mapper)
        {
            Guard.WhenArgument(authorsService, nameof(authorsService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            this.authorsService = authorsService;
            this.mapper = mapper;
        }
        // GET: Administration/Authors
        public ActionResult Index()
        {
            var authors = this.authorsService.GetAuthorsWithDeleted()
                .OrderBy(x => x.Name)
                .To<AdminAuthorViewModel>()
                .ToList();

            return View(authors);
        }
    }
}