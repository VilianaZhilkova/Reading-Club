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

        public void AddDiscussion(Discussion discussion)
        {
            this.discussions.Add(discussion);
            this.unitOfWork.Commit();
        }
    }
}
