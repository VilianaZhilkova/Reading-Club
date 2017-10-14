using System;
using System.Linq;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface ICommentsService
    {
        IQueryable<Comment> GetAllComments();

        IQueryable<Comment> GetAllCommentsWithDeleted();

        Comment GetById(int id);

        void AddComment(string content, DateTime date, User currentUser, Discussion discussion);

        void Update(Comment comment);
    }
}
