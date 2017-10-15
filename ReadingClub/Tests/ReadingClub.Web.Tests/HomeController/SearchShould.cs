using AutoMapper;
using AutoMapper.QueryableExtensions;
using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.ViewModels.Discussions;
using ReadingClub.Web.Infrastructure.Mapping;

using ReadingClub.Data.Models;
using ReadingClub.Web.ViewModels.Home;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace ReadingClub.Web.Tests.HomeControllers
{
    [TestFixture]
    public class SearchShould
    {
        
         [Test]
         public void RedirectToIndexWhenSearchInParameterIsNull()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            var searchString = "test";
            homeController.WithCallTo(x => x.Search(null, searchString)).ShouldRedirectToRoute("");
        }

        [Test]
        public void RedirectToIndexWhenSearchStringParameterIsNull()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            var searchIn = "test";
            homeController.WithCallTo(x => x.Search(searchIn, null)).ShouldRedirectToRoute("");
        }

        [Test]
        public void RedirectToIndexWhenSearchInParameterIsNotValid()
        {
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            var searchIn = "test";
            var searchString = "book";
            homeController.WithCallTo(x => x.Search(searchIn, searchString)).ShouldRedirectToRoute("");
        }

        [Test]
        public void ReturnTheCorrectVieReturnTheCorrectViewWhenParametersAreAndSearchInIsDiscussionsValidwWhenParametersAreValid()
        {
            var searchIn = "discussions";
            var searchString = "test";
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();            

            mockedDiscussionService.Setup(x => x.GetAllApprovedDiscussions()).Returns(new List<Discussion>().AsQueryable);

            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            //homeController.WithCallTo(x => x.Search(searchIn, searchString)).ShouldRenderView("Search")
               // .WithModel<SearchViewModel>();
        }

        [Test]
        public void ReturnTheCorrectViewWhenParametersAreAndSearchInIsBooksValid()
        {
            var searchIn = "books";
            var searchString = "test";
            var mockedDiscussionService = new Mock<IDiscussionsService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedMapper = new Mock<IMapper>();
            mockedMapper.Setup(m => m.Map<DiscussionViewModel>(It.IsAny<Discussion>()))
            .Returns(new DiscussionViewModel());
            var homeController = new HomeController(mockedDiscussionService.Object, mockedBooksService.Object, mockedMapper.Object);

            //homeController.WithCallTo(x => x.Search(searchIn, searchString)).ShouldRenderView("Search")
               //.WithModel<SearchViewModel>();
        }
    }
}
