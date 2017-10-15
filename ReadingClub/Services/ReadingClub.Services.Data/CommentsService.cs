using System;
using System.Linq;

using Bytes2you.Validation;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Services.Data
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> comments;
        private readonly IUnitOfWork unitOfWork;

        public CommentsService(IRepository<Comment> comments, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(comments, nameof(comments)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
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
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();
            return this.comments.GetById(id);
        }

        public void AddComment(string content, DateTime date, User currentUser, Discussion discussion)
        {
            Guard.WhenArgument(content, nameof(content)).IsNullOrWhiteSpace().Throw();
            Guard.WhenArgument(date, nameof(date)).IsEqual(DateTime.UtcNow.AddMinutes(-5)).Throw();
            Guard.WhenArgument(currentUser, nameof(currentUser)).IsNull().Throw();
            Guard.WhenArgument(discussion, nameof(discussion)).IsNull().Throw();

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
            Guard.WhenArgument(comment, nameof(comment)).IsNull().Throw();
            this.comments.Update(comment);
            this.unitOfWork.Commit();
        }
    }
}
