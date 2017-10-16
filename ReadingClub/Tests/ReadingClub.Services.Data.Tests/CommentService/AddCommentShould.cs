using System;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.CommentService
{
    [TestFixture]
    public class AddCommentShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenContentlsNull()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedUser = new Mock<User>();
            var mockedDiscussion = new Mock<Discussion>();

            Assert.That(() => commentsService.AddComment(null, date, mockedUser.Object, mockedDiscussion.Object),
                            Throws.ArgumentNullException.With.Message.Contains("content"));
        }

        [TestCase("")]
        [TestCase(" ")]
        public void ThrowArgumentExceptionWithProperMessageWhenContentIsEmptyOrWhiteSpace(string content)
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedUser = new Mock<User>();
            var mockedDiscussion = new Mock<Discussion>();

            Assert.That(() => commentsService.AddComment(content, date, mockedUser.Object, mockedDiscussion.Object),
                            Throws.ArgumentException.With.Message.Contains("content"));
        }

        [Test]
        public void ThrowArgumentOutOfRangeExceptionWithProperMessageWhenDateIsNotValid()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var content = "test";
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow.AddMinutes(-10);
            var mockedUser = new Mock<User>();
            var mockedDiscussion = new Mock<Discussion>();

            Assert.That(() => commentsService.AddComment(content, date, mockedUser.Object, mockedDiscussion.Object),
                            Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.Contains("date"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenUserIsNull()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var content = "test";
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedDiscussion = new Mock<Discussion>();

            Assert.That(() => commentsService.AddComment(content, date, null, mockedDiscussion.Object),
                            Throws.ArgumentNullException.With.Message.Contains("currentUser"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenDiscussionIsNull()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var content = "test";
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedUser = new Mock<User>();

            Assert.That(() => commentsService.AddComment(content, date, mockedUser.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("discussion"));
        }

        [Test]
        public void CallCommentdRepositoryAddMethodWhenAllParametersAreValid()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var content = "test";
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedUser = new Mock<User>();
            var mockedDiscussion = new Mock<Discussion>();

            commentsService.AddComment(content, date, mockedUser.Object, mockedDiscussion.Object);

            mockedRepository.Verify(x => x.Add(It.IsAny<Comment>()), Times.Once());
        }

        [Test]
        public void CallUnitOfWorkCommintMethodWhenAllParametersAreValid()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var content = "test";
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var date = DateTime.UtcNow;
            var mockedUser = new Mock<User>();
            var mockedDiscussion = new Mock<Discussion>();

            commentsService.AddComment(content, date, mockedUser.Object, mockedDiscussion.Object);

            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}
