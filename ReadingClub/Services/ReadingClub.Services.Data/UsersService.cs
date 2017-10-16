using System.Linq;

using Bytes2you.Validation;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;


using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReadingClub.Services.Data
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> users;
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IRepository<User> users, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(users, nameof(users)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.users = users;
            this.unitOfWork = unitOfWork;
        }

        public User GetUserByUserName(string userName)
        {
            Guard.WhenArgument(userName, nameof(userName)).IsNullOrWhiteSpace().Throw();
            return this.users.GetAll.Where(u => u.UserName == userName).FirstOrDefault();
        }

        public IQueryable<User> GetAllUsers()
        {
            return this.users.GetAll;
        }
    }
}
