using AutoMapper;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Controllers;


namespace ReadingClub.Web.Tests.HomeControllers
{
    [TestFixture]
    public class AboutShould
    {
        [Test]
        public void ReturnView()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            homeController.WithCallTo(x => x.About()).ShouldRenderView("About");
        }
    }
}
