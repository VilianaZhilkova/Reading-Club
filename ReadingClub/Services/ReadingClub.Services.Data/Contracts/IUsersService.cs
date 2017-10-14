using System.Linq;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IUsersService
    {
        User GetUserByUserName(string userName);

        IQueryable<User> GetAllUsers();
    }
}
