using ReadingClub.Data.Models;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.ViewModels.Users
{
    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }
        public string UserName { get; set; }
    }
}