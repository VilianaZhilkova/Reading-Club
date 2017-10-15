using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using System;

namespace ReadingClub.Services.Data.Tests.CommentService
{
    [TestFixture]
    public class UpdateShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenCommentlsNull()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => commentsService.Update(null),
                            Throws.ArgumentNullException.With.Message.Contains("comment"));
        }

        [Test]
        public void CallCommentdRepositoryUpdateMethodWhenCommentIsValid()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedComment = new Mock<Comment>();

            commentsService.Update(mockedComment.Object);

            mockedRepository.Verify(x => x.Update(mockedComment.Object), Times.Once());
        }

        [Test]
        public void CallUnitOfWorkCommintMethodWhenCommentIsValid()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedComment = new Mock<Comment>();

            commentsService.Update(mockedComment.Object);

            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}
