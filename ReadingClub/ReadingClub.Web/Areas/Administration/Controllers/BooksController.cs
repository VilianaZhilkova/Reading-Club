using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;

using AutoMapper;

using ReadingClub.Common;
using ReadingClub.Web.Infrastructure.Mapping;
using ReadingClub.Web.Areas.Administration.ViewModels.Books;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Data.Models;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public BooksController(IBooksService booksService, IMapper mapper)
        {
            this.booksService = booksService;
            this.mapper = mapper;
        }
        // GET: Administration/Books
        public ActionResult Index()
        {
            return RedirectToAction("BooksOnSite");
        }

        [HttpGet]
        public ActionResult BooksOnSite()
        {
            var booksOnSite = this.booksService.GetAllApprovedBooks()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d  => d.Author)
                .ToList();
            return View(booksOnSite);
        }

        [HttpGet]
        public ActionResult DeletedBooks()
        {
            var deletedBooks = this.booksService.GetAllDeletedBooks()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d => d.Author)
                .ToList();
            return View(deletedBooks);
        }

        [HttpGet]
        public ActionResult BooksForApproval()
        {
            var booksForApproval = this.booksService.GetAllBooksForApproval()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d => d.Author)
                .ToList();
            return View(booksForApproval);
        }

        public ActionResult RestoreBook(string bookId)
        {
            var book = this.booksService.GetByIdWithDeleted(int.Parse(bookId));
            book.IsDeleted = false;
            this.booksService.Update(book);

            return RedirectToAction("DeletedBooks");
        }

        public ActionResult DeleteBook(string bookId)
        {
            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsDeleted = true;
            this.booksService.Update(book);

            return RedirectToAction("BooksOnSite");
        }

        public ActionResult DisapproveBook(string bookId)
        {
            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsApproved = false;
            this.booksService.Update(book);

            return RedirectToAction("BooksOnSite");
        }

        public ActionResult ApproveBook(string bookId)
        {
            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsApproved = true;
            this.booksService.Update(book);

            return RedirectToAction("BooksForApproval");
        }
    }
}