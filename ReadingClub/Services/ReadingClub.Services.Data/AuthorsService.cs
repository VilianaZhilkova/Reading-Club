using System.Linq;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Common.Contracts;
using System;

using Bytes2you.Validation;

namespace ReadingClub.Services.Data
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IRepository<Author> authors;
        private readonly IUnitOfWork unitOfWork;
        public AuthorsService(IRepository<Author> authors, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(authors, nameof(authors)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.authors = authors;
            this.unitOfWork = unitOfWork;
        }

        public Author GetBookAuthorByName(string authorName)
        {
            Guard.WhenArgument(authorName, nameof(authorName)).IsNullOrWhiteSpace().Throw();
            return this.authors.GetAll.Where(a => a.Name == authorName).FirstOrDefault();
        }

        public IQueryable<Author> GetAuthorsWithDeleted()
        {
            return this.authors.GetAllWithDeleted;
        }

    }
}
