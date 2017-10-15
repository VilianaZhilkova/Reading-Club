using System.Linq;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IRepository<User> users, IUnitOfWork unitOfWork)
        {
            this.users = users;
            this.unitOfWork = unitOfWork;
        }

        public User GetUserByUserName(string userName)
        {
            return this.users.GetAll.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.users.GetAll;
        }
    }
}
