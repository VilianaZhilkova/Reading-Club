using Microsoft.AspNet.Identity;
using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.UserService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIRepositoryIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.That(() => new UsersService(null, mockedUnitOfWork.Object),
                            Throws.ArgumentNullException.With.Message.Contains("users"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIUnitOfWorkIsNull()
        {
            var mockedRepository = new Mock<IRepository<User>>();

            Assert.That(() => new UsersService(mockedRepository.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("unitOfWork"));
        }

        [Test]
        public void NotThrowWhenIRepositoryAndIUnitOfWorkAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();

            Assert.DoesNotThrow(() => new UsersService(mockedRepository.Object, mockedUnitOfWork.Object));
        }

        [Test]
        public void CreateInstanceOfUsersServiceWhenParametersAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<User>>();

            var usersService = new UsersService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.IsInstanceOf<UsersService>(usersService);
        }
    }
}