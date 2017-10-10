using System;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface ICommentsService
    {
        void AddComment(string content, DateTime date, User currentUser, Discussion discussion);
    }
}
