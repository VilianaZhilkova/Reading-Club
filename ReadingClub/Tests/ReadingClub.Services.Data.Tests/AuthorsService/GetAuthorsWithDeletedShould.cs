using System.Collections.Generic;
using System.Linq;

using Moq;

using NUnit.Framework;

using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Tests.AutorsService
{
    [TestFixture]
    public class GetAuthorsWithDeletedShould
    {
        [Test]
        public void CallAuthorsRepositoryGetAllWithDeletedMethod()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();
            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var result = authorsService.GetAuthorsWithDeleted();

            mockedRepository.Verify(x => x.GetAllWithDeleted, Times.Once());
        }

        [Test]
        public void ReturnCorrectResultWhenThereAreAuthors()
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

            mockedRepository.Setup(x => x.GetAllWithDeleted).Returns(authors.AsQueryable<Author>);

            var result = authorsService.GetAuthorsWithDeleted();

            Assert.AreEqual(authors, result);
        }

        [Test]
        public void ReturnEmptyCollectionWhenThereAreNoAthors()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedRepository = new Mock<IRepository<Author>>();
            var authorsService = new AuthorsService(mockedRepository.Object, mockedUnitOfWork.Object);

            var authors = new List<Author>();

            mockedRepository.Setup(x => x.GetAllWithDeleted).Returns(authors.AsQueryable<Author>);

            var result = authorsService.GetAuthorsWithDeleted();

            Assert.IsEmpty(result);
        }
    }
}
