using System.Linq;
using System.Web.Mvc;

using AutoMapper;
using ReadingClub.Web.Infrastructure.Mapping;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.ViewModels.Books;

namespace ReadingClub.Web.Controllers
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

        public ActionResult Index()
        {
            var books = this.booksService.GetAll().To<BookViewModel>().ToList();
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
    }
}
