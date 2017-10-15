using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.CommentService
{
    [TestFixture]
    public class GetAllCommentsShould
    {
        [Test]
        public void CallCommentsRepositoryGetAllMethod()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = commentsService.GetAllComments();

            mockedRepository.Verify(x => x.GetAll, Times.Once());
        }

        [Test]
        public void ReturnCorrectResultWhenThereAreComments()
        {
            var firstCommentContent = "test";
            var secondCommentContent = "test2";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstComment = new Mock<Comment>().Object;
            mockedFirstComment.Content = firstCommentContent;

            var mockedSecondComment = new Mock<Comment>().Object;
            mockedSecondComment.Content = secondCommentContent;

            var comments = new List<Comment>();
            comments.Add(mockedFirstComment);
            comments.Add(mockedSecondComment);

            mockedRepository.Setup(x => x.GetAll).Returns(comments.AsQueryable<Comment>);

            var result = commentsService.GetAllComments();

            Assert.AreEqual(comments, result);
        }

        [Test]
        public void ReturnEmptyCollectionWhenThereAreNoComments()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var comments = new List<Comment>();

            mockedRepository.Setup(x => x.GetAllWithDeleted).Returns(comments.AsQueryable<Comment>);

            var result = commentsService.GetAllComments();

            Assert.IsEmpty(result);
        }
    }
}
