using AutoMapper;

using Moq;

using NUnit.Framework;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Services.Web.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.Hubs.Data;

namespace ReadingClub.Web.Tests.DiscussionsControllers
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenDiscussionsServiceIsNull()
        {
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new DiscussionsController(null, mockedUsersService.Object, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("discussionsService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenUsersServiceIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new DiscussionsController(mockedDiscussionsService.Object, null, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("usersService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenBooksServiceIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, null, discussionUserData.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenDiscussionUsersDataIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, null, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("discussionUsersData"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenMapperIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockerdDiscussionUserData = new Mock<IDiscussionUsersData>();

            Assert.That(() => new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockerdDiscussionUserData.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void CreateInstanceOfDiscussionsControllerWhenParametersAreNotNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object);
            
            Assert.IsInstanceOf<DiscussionsController>(discussionsController);
        }
    }
}
