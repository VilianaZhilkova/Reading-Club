using System.Linq;

using Bytes2you.Validation;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Services.Data
{
    public class DiscussionsService : IDiscussionsService
    {
        private readonly IRepository<Discussion> discussions;
        private readonly IUnitOfWork unitOfWork;

        public DiscussionsService(IRepository<Discussion> discussions, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(discussions, nameof(discussions)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.discussions = discussions;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Discussion> GetAllApprovedDiscussions()
        {
            return this.discussions.GetAll.Where(x => x.IsApproved);
        }

        public Discussion GetById(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();
            return this.discussions.GetById(id);
        }

        public Discussion GetByIdWithDeleted(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();
            return this.discussions.GetByIdWithDeleted(id);
        }

        public void AddDiscussion(Discussion discussion)
        {
            Guard.WhenArgument(discussion, nameof(discussion)).IsNull().Throw();
            this.discussions.Add(discussion);
            this.unitOfWork.Commit();
        }

        public void AddUserToDiscussion(Discussion discussion, User user)
        {
            Guard.WhenArgument(discussion, nameof(discussion)).IsNull().Throw();
            Guard.WhenArgument(user, nameof(user)).IsNull().Throw();
            discussion.Users.Add(user);
            this.unitOfWork.Commit();
        }

        public void RemoveUserFromDiscussion(Discussion discussion, User user)
        {
            Guard.WhenArgument(discussion, nameof(discussion)).IsNull().Throw();
            Guard.WhenArgument(user, nameof(user)).IsNull().Throw();
            discussion.Users.Remove(user);
            this.unitOfWork.Commit();
        }

        public IQueryable<Discussion> GetAllDeletedDiscussions()
        {
            return this.discussions.GetAllWithDeleted.Where(x => x.IsDeleted);
        }

        public IQueryable<Discussion> GetAllDiscussionsForApproval()
        {
            return this.discussions.GetAll.Where(x => !x.IsApproved);
        }

        public void Update(Discussion discussion)
        {
            Guard.WhenArgument(discussion, nameof(discussion)).IsNull().Throw();
            this.discussions.Update(discussion);
            this.unitOfWork.Commit();
        }
    }
}
