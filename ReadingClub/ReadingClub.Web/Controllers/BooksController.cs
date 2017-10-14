using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using ReadingClub.Web.Infrastructure.Mapping;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Services.Web.Contracts;
using ReadingClub.Web.ViewModels.Books;
using Bytes2you.Validation;

namespace ReadingClub.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IAuthorsService authorsService;
        private readonly ICacheService cacheService;
        private readonly IMapper mapper;

        public BooksController(IBooksService booksService, IAuthorsService authorsService, ICacheService cacheService, IMapper mapper)
        {
            Guard.WhenArgument(booksService, nameof(booksService)).IsNull().Throw();
            Guard.WhenArgument(authorsService, nameof(authorsService)).IsNull().Throw();
            Guard.WhenArgument(cacheService, nameof(cacheService)).IsNull().Throw();
            Guard.WhenArgument(mapper, nameof(mapper)).IsNull().Throw();

            this.booksService = booksService;
            this.authorsService = authorsService;
            this.cacheService = cacheService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var books = this.cacheService.Get("books", () =>
            this.booksService.GetAllApprovedBooks()
                                .OrderBy(b => b.Title)
                                .ThenBy(b => b.Author)
                                .To<BookViewModel>()
                                .ToList(), 30 * 60);
            return View(books);
        }

        [HttpGet]
        public ActionResult GetById(int? bookId)
        {
            if(bookId == null)
            {
                return RedirectToAction("Index");
            }

            var book = this.booksService.GetById((int)bookId);
            var model = this.mapper.Map<DetailBookViewModel>(book);

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook(AddBookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = mapper.Map<Book>(model);

                var author = this.authorsService.GetBookAuthorByName(model.AuthorName);
                if(author == null)
                {
                    author = new Author { Name = model.AuthorName };
                }
                book.Author = author;

                this.booksService.AddBook(book);

                return RedirectToAction("GetById", "Books", new { bookId = book.Id });
            }

            return View(model);

        }
    }
}
