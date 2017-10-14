using System;
using System.Linq;

using ReadingClub.Data.Models;
using ReadingClub.Data.Common.Contracts;
using ReadingClub.Services.Data.Contracts;

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

        public IQueryable<Comment> GetAllComments()
        {
            return this.comments.GetAll;
        }

        public IQueryable<Comment> GetAllCommentsWithDeleted()
        {
            return this.comments.GetAllWithDeleted;
        }

        public Comment GetById(int id)
        {
            return this.comments.GetById(id);
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

        public void Update(Comment comment)
        {
            this.comments.Update(comment);
            this.unitOfWork.Commit();
        }
    }
}
