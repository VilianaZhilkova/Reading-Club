using AutoMapper;

using Moq;

using NUnit.Framework;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Services.Web.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.Hubs.Data;
using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.ViewModels.Discussions;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadingClub.Web.Tests.BookController
{
    [TestFixture]
    public class GetById
    {
        [Test]
        public void RedirectToIndexWhenBookIdIsNull()
        {
            var mockedBooksService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            var booksController = new BooksController(mockedBooksService.Object, mockedAuthorService.Object, mockedCacheService.Object, mockedMapper.Object);

            booksController.WithCallTo(x => x.GetById(null)).ShouldRedirectToRoute("");
        }

        [Test]
        public void CallBookServiceGetByIdWhenBookIdIsValid()
        {
            var mockedBooksService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            mockedBooksService.Setup(x => x.GetById(1)).Returns(new Book());
            mockedMapper.Setup(m => m.Map<DetailBookViewModel>(It.IsAny<Book>())).Returns(new DetailBookViewModel());
            var booksController = new BooksController(mockedBooksService.Object, mockedAuthorService.Object, mockedCacheService.Object, mockedMapper.Object);

            booksController.GetById(1);

            mockedBooksService.Verify(x => x.GetById(1), Times.Once());
        }


        [Test]
        public void ReturnViewWhitModelWhenBookIdIsNotNull()
        {
            var mockedBooksService = new Mock<IBooksService>();
            var mockedAuthorService = new Mock<IAuthorsService>();
            var mockedCacheService = new Mock<ICacheService>();
            var mockedMapper = new Mock<IMapper>();

            mockedBooksService.Setup(x => x.GetById(1)).Returns(new Book());
            mockedMapper.Setup(m => m.Map<DetailBookViewModel>(It.IsAny<Book>())).Returns(new DetailBookViewModel());
            var booksController = new BooksController(mockedBooksService.Object, mockedAuthorService.Object, mockedCacheService.Object, mockedMapper.Object);

            booksController.GetById(1);
            booksController.WithCallTo(x => x.GetById(1)).ShouldRenderView("GetById").WithModel<DetailBookViewModel>();
        }
    }
}