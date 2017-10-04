using System.Linq;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Common.Contracts;

namespace ReadingClub.Services.Data
{
    public class AuthorsService : IAuthorsService
    {
        private readonly IRepository<Author> authors;
        private readonly IUnitOfWork unitOfWork;
        public AuthorsService(IRepository<Author> authors, IUnitOfWork unitOfWork)
        {
            this.authors = authors;
            this.unitOfWork = unitOfWork;
        }

        public Author GetBookAuthorByName(string authorName)
        {
            return this.authors.GetAll.Where(a => a.Name == authorName).FirstOrDefault();

        }
    }
}
