using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.DiscussionService
{
    [TestFixture]
    public class AddDiscussionShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenDiscussionIsNull()
        {
            var mockedRepository = new Mock<IRepository<Discussion>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var discussionsService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => discussionsService.AddDiscussion(null),
                            Throws.ArgumentNullException.With.Message.Contains("discussion"));
        }

        [Test]
        public void CallDiscussionRepositoryAddMethodWhenDiscussionParameterIsValid()
        {
            var mockedRepository = new Mock<IRepository<Discussion>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var discussionsService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedDiscussion = new Mock<Discussion>();

            discussionsService.AddDiscussion(mockedDiscussion.Object);

            mockedRepository.Verify(x => x.Add(mockedDiscussion.Object), Times.Once());
        }

        [Test]
        public void CallUnitOfWorkCommitMethodWhenDiscussionParameterIsValid()
        {
            var mockedRepository = new Mock<IRepository<Discussion>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var discussionsService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedDiscussion = new Mock<Discussion>();

            discussionsService.AddDiscussion(mockedDiscussion.Object);

            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}
