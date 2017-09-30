using System.Linq;

using ReadingClub.Data.Models;

namespace ReadingClub.Services.Data.Contracts
{
    public interface IBooksService
    {
        IQueryable<Book> GetAll();
    }
}
