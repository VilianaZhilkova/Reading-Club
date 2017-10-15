using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.CommentService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIRepositoryTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.That(() => new CommentsService(null, mockedUnitOfWork.Object),
                            Throws.ArgumentNullException.With.Message.Contains("comments"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIUnitOfWorkIsNull()
        {
            var mockedRepository = new Mock<IRepository<Comment>>();

            Assert.That(() => new CommentsService(mockedRepository.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("unitOfWork"));
        }

        [Test]
        public void NotThrowWhenIRepositoryAndIUnitOfWorkAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();

            Assert.DoesNotThrow(() => new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object));
        }

        [Test]
        public void CreateInstanceOfCommentsServiceWhenParametersAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Comment>>();

            var commentsService = new CommentsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.IsInstanceOf<CommentsService>(commentsService);
        }
    }
}
