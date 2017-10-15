using System;

using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using System.Collections.Generic;

namespace ReadingClub.Services.Data.Tests.CommentService
{
    [TestFixture]
    public class GetByIdShould
    {
        [TestCase(0)]
        [TestCase(-1)]
        public void ThrowArgumentArgumentOutOfRangeExceptionWithProperMessageWhenIdParameterTIsNull(int id)
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();

            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => commentsService.GetById(id),
                            Throws.TypeOf<ArgumentOutOfRangeException>().With.Message.Contains("id"));
        }

        [Test]
        public void CallCommentddRepositoryGetAllMethodWhenAuthorNameParameterIsValid()
        {
            var commentId = 1;

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var authorsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = authorsService.GetById(commentId);

            mockedRepository.Verify(x => x.GetById(commentId), Times.Once());
        }

        [Test]
        public void ReturnNullIfCommentDoesNotExistWhenAuthorNameParameterIsValid()
        {
            var firstCommentId = 1;

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            mockedRepository.Setup(x => x.GetById(firstCommentId)).Returns((Comment)null);
            var result = commentsService.GetById(firstCommentId);

            Assert.IsNull(result);
        }

        [Test]
        public void ReturnCorrectAuthorWhenAuthorNameParameterIsValid()
        {
            var commentId = 1;

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();
            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedComment = new Mock<Comment>().Object;
            mockedComment.Id = commentId;

            mockedRepository.Setup(x => x.GetById(commentId)).Returns(mockedComment);

            var result = commentsService.GetById(commentId);

            Assert.AreSame(mockedComment, result);
        }
    }
}
