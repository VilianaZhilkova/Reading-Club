using System.Linq;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IDiscussionsService
    {
        IQueryable<Discussion> GetAllApprovedDiscussions();

        void AddDiscussion(Discussion discussion);
    }
}
