using Microsoft.AspNet.Identity;
using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;
using System;
using System.Collections.Generic;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Users
{
    public class AdminUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}