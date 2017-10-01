using System.Linq;
using System.Web.Mvc;

using AutoMapper.QueryableExtensions;

using ReadingClub.Data.Models;
using ReadingClub.Services.Data.Contracts;
using ReadingClub.Web.ViewModels.Books;
using ReadingClub.Web.Infrastructure.Mapping.Contracts;

namespace ReadingClub.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService booksService;
        private readonly IMapperProvider mapper;

        public BooksController(IBooksService booksService, IMapperProvider mapper)
        {
            this.booksService = booksService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var books = this.booksService.GetAll().ProjectTo<BookViewModel>().ToList();

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