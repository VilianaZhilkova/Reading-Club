using AutoMapper;

using Moq;

using NUnit.Framework;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Controllers;
using ReadingClub.Web.Hubs.Data;
using ReadingClub.Web.ViewModels.Discussions;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;

namespace ReadingClub.Web.Tests.DiscussionController
{
    [TestFixture]
    public class GetByIdShould
    {
        [Test]
        public void RedirectToIndexWhenDiscussionIdIsNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var discussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, discussionUserData.Object, mockedMapper.Object);

            discussionsController.WithCallTo(x => x.GetById(null)).ShouldRedirectToRoute("").WithRouteValue("discussionStatus");
        }

        [Test]
        public void CallDiscussionDataGetDataWhenDiscussionIdIsNotNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedDiscussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();
            var mockedDiscussion = new Mock<Discussion>();
            var mockedComment = new Mock<Comment>();

            mockedDiscussionsService.Setup(x => x.GetById(1)).Returns(mockedDiscussion.Object);
            mockedMapper.Setup(m => m.Map<DetailDiscussionViewModel>(mockedDiscussion));
            mockedDiscussion.SetupGet(m => m.Comments).Returns(new List<Comment>() { mockedComment.Object });
            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockedDiscussionUserData.Object, mockedMapper.Object);

            discussionsController.GetById(1);

            mockedDiscussionUserData.Verify(x => x.GetData(), Times.Once());
        }

        [Test]
        public void CallDiscussionServiceGetByIdWhenDiscussionStatusIsNotNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedDiscussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();
            var mockedDiscussion = new Mock<Discussion>();
            var mockedComment = new Mock<Comment>();

            mockedDiscussionsService.Setup(x => x.GetById(1)).Returns(mockedDiscussion.Object);
            mockedMapper.Setup(m => m.Map<DetailDiscussionViewModel>(mockedDiscussion));
            mockedDiscussion.SetupGet(m => m.Comments).Returns(new List<Comment>() { mockedComment.Object});
            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockedDiscussionUserData.Object, mockedMapper.Object);

            discussionsController.GetById(1);

            mockedDiscussionsService.Verify(x => x.GetById(1), Times.Once());
        }

        [Test]
        public void ReturnViewWhitModelWhenDiscussionIdIsNotNull()
        {
            var mockedDiscussionsService = new Mock<IDiscussionsService>();
            var mockedUsersService = new Mock<IUsersService>();
            var mockedBooksService = new Mock<IBooksService>();
            var mockedDiscussionUserData = new Mock<IDiscussionUsersData>();
            var mockedMapper = new Mock<IMapper>();

            mockedDiscussionsService.Setup(x => x.GetById(1)).Returns(new Discussion());

            mockedMapper.Setup(m => m.Map<DetailDiscussionViewModel>(It.IsAny<Discussion>())).Returns(new DetailDiscussionViewModel());
            var discussionsController = new DiscussionsController(mockedDiscussionsService.Object, mockedUsersService.Object, mockedBooksService.Object, mockedDiscussionUserData.Object, mockedMapper.Object);
  
            discussionsController.GetById(1);

            discussionsController.WithCallTo(x => x.GetById(1)).ShouldRenderView("GetById").WithModel<DetailDiscussionViewModel>();
        }
    }
}
