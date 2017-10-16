using AutoMapper;

using Moq;

using NUnit.Framework;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.Hubs.Data;
using TestStack.FluentMVCTesting;

namespace ReadingClub.Web.Tests.DiscussionController
{
    [TestFixture]
    public class HttpGetCreateDiscussionShould
    {
        [Test]
        public void ReturnViewWhenCalledWithValidParameters()
        {
            var bookId = 1;
            var bookTitle = "test";

            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedDiscussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();
            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockedDiscussionUserData.Object, mockedMapper.Object);

            discussionsController.WithCallTo(x => x.CreateDiscussion(bookId, bookTitle)).ShouldRenderDefaultView();
        }

        [TestCase(null, "test")]
        [TestCase(1, null)]
        public void ReturnViewWhenCalledWithInvalidValidParameters(int? bookId, string bookTitle)
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedDiscussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();
            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockedDiscussionUserData.Object, mockedMapper.Object);

            discussionsController.WithCallTo(x => x.CreateDiscussion(bookId, bookTitle)).ShouldRedirectTo<BooksController>(typeof(BooksController).GetMethod("Index"));
        }
    }
}
