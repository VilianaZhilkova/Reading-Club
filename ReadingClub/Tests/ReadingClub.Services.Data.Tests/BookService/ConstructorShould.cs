using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;


namespace ReadingClub.Services.Data.Tests.BookService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIRepositoryTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.That(() => new BooksService(null, mockedUnitOfWork.Object),
                            Throws.ArgumentNullException.With.Message.Contains("books"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIUnitOfWorkIsNull()
        {
            var mockedRepository = new Mock<IRepository<Book>>();

            Assert.That(() => new BooksService(mockedRepository.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("unitOfWork"));
        }

        [Test]
        public void NotThrowWhenIRepositoryAndIUnitOfWorkAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();

            Assert.DoesNotThrow(() => new BooksService(mockedRepository.Object, mockedUnitOfWork.Object));
        }

        [Test]
        public void CreateInstanceOfBooksServiceWhenParametersAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();

            var booksService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.IsInstanceOf<BooksService>(booksService);
        }
    }
}
