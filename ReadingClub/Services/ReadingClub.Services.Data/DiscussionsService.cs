using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Services.Data
{
    public class DiscussionsService: IDiscussionsService
    {
        private readonly IRepository<Discussion> discussions;
        private readonly IUnitOfWork unitOfWork;
        public DiscussionsService(IRepository<Discussion> discussions, IUnitOfWork unitOfWork)
        {
            this.discussions = discussions;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Discussion> GetAllApprovedDiscussions()
        {
            return this.discussions.GetAll.Where(x => x.IsApproved);
        }

        public Discussion GetById(int id)
        {
            return this.discussions.GetById(id);
        }
        public void AddDiscussion(Discussion discussion)
        {
            this.discussions.Add(discussion);
            this.unitOfWork.Commit();
        }

        public void AddUserToDiscussion(Discussion discussion, User user)
        {
            discussion.Users.Add(user);
            this.unitOfWork.Commit();
        }

        public void RemoveUserFromDiscussion(Discussion discussion, User user)
        {
            discussion.Users.Remove(user);
            this.unitOfWork.Commit();
        }
    }
}
