using AutoMapper;

using Moq;

using NUnit.Framework;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Services.Web.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.Hubs.Data;
using TestStack.FluentMVCTesting;

namespace ReadingClub.Web.Tests.DiscussionController
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void RedirectToIndexWhenDiscussionStatusIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object);

            discussionsController.WithCallTo(x => x.Index(null)).ShouldRedirectToRoute("");
        }

        [TestCase("upcoming")]
        [TestCase("current")]
        [TestCase("passed")]
        public void ReturnViewWhenDiscussionStatusIsValid(string discussionStatus)
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object);

            discussionsController.WithCallTo(x => x.Index(discussionStatus)).ShouldRenderView("Index");
        }
    }
}
