using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.Areas.Administration.ViewModels.Users
{
    public class AdminUserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}