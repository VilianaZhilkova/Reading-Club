using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using System.Collections.Generic;

namespace ReadingClub.Services.Data.Tests.DiscussionService
{
    [TestFixture]
    public class RemoveUserFromDiscussionShould
    {
        [Test]
        public void ThrowWhenDiscussionIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Discussion>>();
            var mockedUser = new Mock<User>();
            var discussionsService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => discussionsService.RemoveUserFromDiscussion(null, mockedUser.Object),
                    Throws.ArgumentNullException.With.Message.Contains("discussion"));
        }

        [Test]
        public void ThrowWhenUserIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Discussion>>();
            var mockedDiscussion = new Mock<Discussion>();
            var discussionsService = new DiscussionsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => discussionsService.RemoveUserFromDiscussion(mockedDiscussion.Object, null),
                    Throws.ArgumentNullException.With.Message.Contains("user"));
        }

    }
}
