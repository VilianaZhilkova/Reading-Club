using AutoMapper;

using Moq;

using NUnit.Framework;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Controllers;

namespace ReadingClub.Web.Tests.HomeControllers
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenDiscussionsServiceIsNull()
        {
            var mockedBookService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();
            
            Assert.That(() => new HomeController(null, mockedBookService.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("discussionsService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenBooksServiceIsNull()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new HomeController(mockedDiscussionService.Object, null, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenMapperIsNull()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();

            Assert.That(() => new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void CreateInstanceOfHomeControllerWhenParametersAreNotNull()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            Assert.IsInstanceOf<HomeController>(homeController);
        }
    }
}
