using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.BookService
{
    [TestFixture]
    public class AddBookShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenBookIsNull()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => booksService.AddBook(null),
                            Throws.ArgumentNullException.With.Message.Contains("book"));
        }

        [Test]
        public void CallBookdRepositoryAddMethodWhenBookParameterIsValid()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksSercvice = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedBook = new Mock<Book>();

            booksSercvice.AddBook(mockedBook.Object);

            mockedRepository.Verify(x => x.Add(mockedBook.Object), Times.Once());
        }

        [Test]
        public void CallUnitOfWorkCommintMethodWhenBookParameterIsValid()
        {
            var mockedRepository = new Mock<IRepository<Book>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var booksSercvice = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);
            var mockedBook = new Mock<Book>();

            booksSercvice.AddBook(mockedBook.Object);

            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once());
        }
    }
}
