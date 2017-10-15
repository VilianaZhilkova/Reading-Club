using AutoMapper;

using Moq;

using NUnit.Framework;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Services.Web.Contracts;
using ReadingClub.Web.Controllers;

namespace ReadingClub.Web.Tests.BooksControllers
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenBooksServiceIsNull()
        {
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new BooksController(null, mockedAuthorService.Object, mockedCacheService.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("booksService"));
        }
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenAuthorServiceIsNull()
        {
            var mockedBookService = new Mock<IBooksService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new BooksController(mockedBookService.Object, null, mockedCacheService.Object, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("authorsService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenCacheServiceIsNull()
        {
            var mockedBookService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedMapper = new Mock<IMapper>();

            Assert.That(() => new BooksController(mockedBookService.Object, mockedAuthorService.Object, null, mockedMapper.Object),
                            Throws.ArgumentNullException.With.Message.Contains("cacheService"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenMapperIsNull()
        {
            var mockedBooksService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();

            Assert.That(() => new BooksController(mockedBooksService.Object, mockedAuthorService.Object, mockedCacheService.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("mapper"));
        }

        [Test]
        public void CreateInstanceOfBooksControllerWhenParametersAreNotNull()
        {
            var mockedBooksService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            var booksController = new BooksController(mockedBooksService.Object, mockedAuthorService.Object, mockedCacheService.Object, mockedMapper.Object);

            Assert.IsInstanceOf<BooksController>(booksController);
        }
    }
}
