using ReadingClub.Data.Models;
using ReadingClub.Data.Common.Contracts;
using ReadingClub.Services.Data.Contracts;
using System;

namespace ReadingClub.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> comments;
        private readonly IUnitOfWork unitOfWork;
        public CommentsService(IRepository<Comment> comments, IUnitOfWork unitOfWork)
        {
            this.comments = comments;
            this.unitOfWork = unitOfWork;
        }
        public void AddComment(string content, DateTime date, User currentUser, Discussion discussion)
        {
            var comment = new Comment
            {
                Author = currentUser,
                Date = date,
                Content = content,
                Discussion = discussion
            };

            this.comments.Add(comment);
            this.unitOfWork.Commit();
        }
    }
}
