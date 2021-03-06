﻿using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.AutorsService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIRepositoryTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.That(() => new AuthorsService(null, mockedUnitOfWork.Object),
                            Throws.ArgumentNullException.With.Message.Contains("authors"));
        }

        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenIUnitOfWorkIsNull()
        {
            var mockedRepository = new Mock<IRepository<Author>>();

            Assert.That(() => new AuthorsService(mockedRepository.Object, null),
                            Throws.ArgumentNullException.With.Message.Contains("unitOfWork"));
        }

        [Test]
        public void NotThrowWhenIRepositoryAndIUnitOfWorkAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();

            Assert.DoesNotThrow(() => new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object));
        }

        [Test]
        public void CreateInstanceOfAuthorServiceWhenParametersAreNotNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();

            var authorService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.IsInstanceOf<AuthorsService>(authorService);
        }
    }
}
