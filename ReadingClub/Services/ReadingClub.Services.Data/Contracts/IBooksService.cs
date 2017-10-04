using System.Linq;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IBooksService
    {
        IQueryable<Book> GetAllApprovedBooks();
        Book GetById(int id);
        void AddBook(Book book);
    }
}
