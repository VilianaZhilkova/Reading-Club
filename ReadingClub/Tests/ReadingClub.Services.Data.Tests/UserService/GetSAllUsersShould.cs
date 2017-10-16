using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.UserService
{
    [TestFixture]
    public class GetSAllUsersShould
    {
        [Test]
        public void CallUsersRepositoryGetAllMethod()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();
            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = usersService.GetAllUsers();

            mockedRepository.Verify(x => x.GetAll, Times.Once());
        }

        [Test]
        public void ReturnCorrectResultWhenThereAreUsers()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();
            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstUser = new Mock<User>().Object;
            var mockedSecondUser= new Mock<User>().Object;

            var users = new List<User>();
            users.Add(mockedFirstUser);
            users.Add(mockedSecondUser);

            mockedRepository.Setup((IRepository<User> x) => x.GetAll).Returns(users.AsQueryable<ReadingClub.Data.Models.User>);

            var result = usersService.GetAllUsers();

            Assert.AreEqual(users, result);
        }

        [Test]
        public void ReturnEmptyCollectionWhenThereAreNoUsers()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();
            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            var users = new List<User>();

            mockedRepository.Setup((IRepository<User> x) => x.GetAllWithDeleted).Returns(users.AsQueryable<ReadingClub.Data.Models.User>);

            var result = usersService.GetAllUsers();

            Assert.IsEmpty(result);
        }
    }
}
