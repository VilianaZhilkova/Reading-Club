using Moq;
using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ReadingClub.Services.Data.Tests.BookService
{
    [TestFixture]
    public class GetAllBooksForApproval
    {
        [Test]
        public void CallBookRepositoryGetAllMethod()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();
            var bookService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = bookService.GetAllBooksForApproval();

            mockedRepository.Verify(x => x.GetAll, Times.Once());
        }

        [Test]
        public void ReturnCorrectResultWhenThereAreBooksForApproval()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();
            var bookService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstBook = new Mock<Book>().Object;
            mockedFirstBook.IsApproved = false;

            var mockedSecondBook = new Mock<Book>().Object;
            mockedSecondBook.IsApproved = true;

            var books = new List<Book>();
            books.Add(mockedFirstBook);
            books.Add(mockedSecondBook);

            mockedRepository.Setup(x => x.GetAll).Returns(books.AsQueryable<Book>);
            
            var result = bookService.GetAllBooksForApproval().ToList();

            Assert.AreEqual(mockedFirstBook, result[0]);
        }

        [Test]
        public void ReturnEmptyCollectionWhenThereAreNoBooks()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();
            var bookService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            var books = new List<Book>();

            mockedRepository.Setup(x => x.GetAllWithDeleted).Returns(books.AsQueryable<Book>);

            var result = bookService.GetAllBooksForApproval();

            Assert.IsEmpty(result);
        }

        [Test]
        public void ReturnCorrectResultWhenThereAreNoBooksForApproval()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Book>>();
            var bookService = new BooksService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstBook = new Mock<Book>().Object;
            mockedFirstBook.IsApproved = true;

            var mockedSecondBook = new Mock<Book>().Object;
            mockedSecondBook.IsApproved = true;

            var books = new List<Book>();
            books.Add(mockedFirstBook);
            books.Add(mockedSecondBook);

            mockedRepository.Setup(x => x.GetAll).Returns(books.AsQueryable<Book>);

            var result = bookService.GetAllBooksForApproval().ToList();

            Assert.IsEmpty(result);
        }

    }
}
