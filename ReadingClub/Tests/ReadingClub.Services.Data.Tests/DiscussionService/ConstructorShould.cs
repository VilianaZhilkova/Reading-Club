using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.DiscussionService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIRepositoryTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.That(() => new DiscussionsService(null, mockedUnitOfWork.Object),
                            Throws.ArgumentNullException.With.Message.Contains("discussions"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIUnitOfWorkIsNull()
        {
            var mockedRepository = new Mock<IRepository<Discussion>>();

            Assert.That(() => new DiscussionsService(mockedRepository.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("unitOfWork"));
        }

        [Test]
        public void NotThrowWhenIRepositoryAndIUnitOfWorkAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Discussion>>();

            Assert.DoesNotThrow(() => new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object));
        }

        [Test]
        public void CreateInstanceOfDiscussionsServiceWhenParametersAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Discussion>>();

            var dicussionService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.IsInstanceOf<DiscussionsService>(dicussionService);
        }
    }
}
