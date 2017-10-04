﻿using ReadingClub.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReadingClub.Data.Common.Contracts;
using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;

namespace ReadingClub.Services.Data
{
    public class BooksService: IBooksService
    {
        private readonly IRepository<Book> books;
        private readonly IUnitOfWork unitOfWork;
        public BooksService(IRepository<Book> books, IUnitOfWork unitOfWork)
        {
            this.books = books;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Book> GetAllApprovedBooks()
        {
            return this.books.GetAll.Where(x => x.IsApproved == true);
        }

        public Book GetById(int id)
        {
            return this.books.GetById(id);
        }

        public void AddBook(Book book)
        {
            this.books.Add(book);
            this.unitOfWork.Commit();
        }
    }
}
