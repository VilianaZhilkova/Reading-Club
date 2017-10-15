using System.Linq;
using System.Web.Mvc;

using AutoMapper;

using Bytes2you.Validation;

using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.Areas.Administration.ViewModels.Books;
using ReadingClub.Web.Infrastructure.Mapping;

namespace ReadingClub.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapper mapper;

        public BooksController(IBooksService booksService, IMapper mapper)
        {
            Guard.WhenArgument(booksService, nameof(booksService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();
            this.booksService = booksService;
            this.mapper = mapper;
        }

        // GET: Administration/Books
        public ActionResult Index()
        {
            return this.RedirectToAction("BooksOnSite");
        }

        [HttpGet]
        public ActionResult BooksOnSite()
        {
            var booksOnSite = this.booksService.GetAllApprovedBooks()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d  => d.Author)
                .ToList();
            return this.View(booksOnSite);
        }

        [HttpGet]
        public ActionResult DeletedBooks()
        {
            var deletedBooks = this.booksService.GetAllDeletedBooks()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d => d.Author)
                .ToList();
            return this.View(deletedBooks);
        }

        [HttpGet]
        public ActionResult BooksForApproval()
        {
            var booksForApproval = this.booksService.GetAllBooksForApproval()
                .To<AdminBookViewModel>()
                .OrderBy(d => d.Title)
                .ThenBy(d => d.Author)
                .ToList();
            return this.View(booksForApproval);
        }

        public ActionResult RestoreBook(string bookId)
        {
            if (bookId == null)
            {
                return this.RedirectToAction("DeletedBooks");
            }

            var book = this.booksService.GetByIdWithDeleted(int.Parse(bookId));
            book.IsDeleted = false;
            this.booksService.Update(book);

            return this.RedirectToAction("DeletedBooks");
        }

        public ActionResult DeleteBook(string bookId)
        {
            if (bookId == null)
            {
                return this.RedirectToAction("BooksOnSite");
            }

            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsDeleted = true;
            this.booksService.Update(book);

            return this.RedirectToAction("BooksOnSite");
        }

        public ActionResult DisapproveBook(string bookId)
        {
            if (bookId == null)
            {
                return this.RedirectToAction("BooksOnSite");
            }

            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsApproved = false;
            this.booksService.Update(book);

            return this.RedirectToAction("BooksOnSite");
        }

        public ActionResult ApproveBook(string bookId)
        {
            if (bookId == null)
            {
                return this.RedirectToAction("BooksForApproval");
            }

            var book = this.booksService.GetById(int.Parse(bookId));
            book.IsApproved = true;
            this.booksService.Update(book);

            return this.RedirectToAction("BooksForApproval");
        }
    }
}