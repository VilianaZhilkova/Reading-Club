using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReadingClub.Services.Data.Contracts;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ReadingClub.Web.ViewModels.Books;

namespace ReadingClub.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public BooksController(IBooksService booksService)
        {
            this.booksService = booksService;
        }

        // GET: Books
        public ActionResult Index()
        {
            var books = this.booksService.GetAll().ProjectTo<BookViewModel>().ToList();

            return View(books);
        }
    }
}