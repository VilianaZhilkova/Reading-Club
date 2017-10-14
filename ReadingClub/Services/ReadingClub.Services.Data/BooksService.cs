using ReadingClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using Bytes2you.Validation;

namespace ReadingClub.Services.Data
{
    public class BooksService: IBooksService
    {
        private readonly IRepository<Book> books;
        private readonly IUnitOfWork unitOfWork;
        public BooksService(IRepository<Book> books, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(books, nameof(books)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.books = books;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Book> GetAllApprovedBooks()
        {
            return this.books.GetAll.Where(x => x.IsApproved == true);
        }

        public Book GetById(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();
            return this.books.GetById(id);
        }

        public Book GetByIdWithDeleted(int id)
        {
            Guard.WhenArgument(id, nameof(id)).IsLessThanOrEqual(0).Throw();
            return this.books.GetByIdWithDeleted(id);
        }

        public void AddBook(Book book)
        {
            Guard.WhenArgument(book, nameof(book)).IsNull().Throw();
            this.books.Add(book);
            this.unitOfWork.Commit();
        }

        public IQueryable<Book> GetAllDeletedBooks()
        {
            this.books.GetAll.Where(x => x.IsDeleted);
            return this.books.GetAllWithDeleted.Where(x => x.IsDeleted); ;
        }

        public IQueryable<Book> GetAllBooksForApproval()
        {
            return this.books.GetAll.Where(x => !(x.IsApproved));
        }

        public void Update(Book book)
        {
            Guard.WhenArgument(book, nameof(book)).IsNull().Throw();
            this.books.Update(book);
            this.unitOfWork.Commit();
        }
    }
}
