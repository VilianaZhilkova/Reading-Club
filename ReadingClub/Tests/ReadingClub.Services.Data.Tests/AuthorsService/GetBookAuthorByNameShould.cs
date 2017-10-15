using System;
using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.AutorsService
{
    [TestFixture]
    public class GetBookAuthorByNameShould
    {
        [Test]
        public void ThrowArgumentNullExceptionWithProperMessageWhenAuthorNameParameterTIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();

            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.That(() => authorsService.GetBookAuthorByName(null),
                            Throws.ArgumentNullException.With.Message.Contains("null"));
        }

        [TestCase("")]
        [TestCase(" ")]
        public void ThrowArgumentExceptionWhenAuthorNameParameterIsEmptyStringOrWhiteSpace(string invalidAuthorName)
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();

            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            Assert.Throws<ArgumentException>(() => authorsService.GetBookAuthorByName(invalidAuthorName));
        }

        [Test]
        public void CallAutorsRepositoryGetAllMethodWhenAuthorNameParameterIsValid()
        {
            var authorName = "test";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();
            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = authorsService.GetBookAuthorByName(authorName);

            mockedRepository.Verify(x => x.GetAll, Times.Once());
        }

        [Test]
        public void ReturnNullIfAuthorDoesNotExistWhenAuthorNameParameterIsValid()
        {
            var firstAuthorName = "test";
            var secondAuthorName = "test2";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();
            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstAuthor = new Mock<Author>().Object;
            mockedFirstAuthor.Name = firstAuthorName;

            var authors = new List<Author>();
            authors.Add(mockedFirstAuthor);

            mockedRepository.Setup(x => x.GetAll).Returns(authors.AsQueryable<Author>);

            var result = authorsService.GetBookAuthorByName(secondAuthorName);

            Assert.IsNull(result);
        }

        [Test]
        public void ReturnCorrectAuthorWhenAuthorNameParameterIsValid()
        {
            var firstAuthorName = "test";
            var secondAuthorName = "test2";

            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();
            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var mockedFirstAuthor = new Mock<Author>().Object;
            mockedFirstAuthor.Name = firstAuthorName;

            var mockedSecondAuthor = new Mock<Author>().Object;
            mockedSecondAuthor.Name = secondAuthorName;

            var authors = new List<Author>();
            authors.Add(mockedFirstAuthor);
            authors.Add(mockedSecondAuthor);

            mockedRepository.Setup(x => x.GetAll).Returns(authors.AsQueryable<Author>);

            var result = authorsService.GetBookAuthorByName(firstAuthorName);

            Assert.AreSame(mockedFirstAuthor, result);
        }
    }
}
