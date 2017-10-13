﻿using System.Linq;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IDiscussionsService
    {
        IQueryable<Discussion> GetAllApprovedDiscussions();

        Discussion GetById(int id);

        void AddDiscussion(Discussion discussion);

        void AddUserToDiscussion(Discussion discussion, User user);

        void RemoveUserFromDiscussion(Discussion discussion, User user);

        IQueryable<Discussion> GetAllDeletedDiscussion();

        IQueryable<Discussion> GetAllDiscussionsWaitingForApprovement();

        void Update(Discussion discussion);
    }
}
