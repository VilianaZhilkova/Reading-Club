using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Common.Contracts;

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
    }
}
