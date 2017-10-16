using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.UserService
{
    [TestFixture]
    public class GetUserByUserNameShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenUserUserNameParameterTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();

            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => usersService.GetUserByUserName(null),
                            Throws.ArgumentNullException.With.Message.Contains("userName"));
        }

        [TestCase("")]
        [TestCase(" ")]
        public void ThrowArgumentExceptionWhenUserUserNameParameterIsEmptyStringOrWhiteSpace(string invalidUserName)
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();

            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => usersService.GetUserByUserName(invalidUserName),
                Throws.ArgumentException.With.Message.Contains("userName"));
        }

        [Test]
        public void CallUserRepositoryGetAllMethodWhenUserUserNameParameterIsValid()
        {
            var userName = "test";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();
            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = usersService.GetUserByUserName(userName);

            mockedRepository.Verify(x => x.GetAll, Times.Once());
        }

        [Test]
        public void ReturnNullIfAuthorDoesNotExistWhenUserUserNameParameterIsValid()
        {
            var firstUserName = "test";
            var secondUserName = "test2";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();
            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstUser = new Mock<User>().Object;
            mockedFirstUser.UserName = firstUserName;

            var users = new List<User>();
            users.Add(mockedFirstUser);

            mockedRepository.Setup(x => x.GetAll).Returns(users.AsQueryable<User>);

            var result = usersService.GetUserByUserName(secondUserName);

            Assert.IsNull(result);
        }

        }
    }
