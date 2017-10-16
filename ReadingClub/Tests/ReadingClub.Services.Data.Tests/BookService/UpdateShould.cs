using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.BookService
{
    [TestFixture]
    public class UpdateShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenBooklsNull()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => booksService.Update(null),
                            Throws.ArgumentNullException.With.Message.Contains("book"));
        }

        [Test]
        public void CallBookdRepositoryUpdateMethodWhenBookIsValid()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedBook = new Mock<Book>();

            booksService.Update(mockedBook.Object);

            mockedRepository.Verify(x => x.Update(mockedBook.Object), Times.Once());
        }

        [Test]
        public void CallUnitOfWorkCommintMethodWhenBookIsValid()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedBook = new Mock<Book>();

            booksService.Update(mockedBook.Object);

            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}
