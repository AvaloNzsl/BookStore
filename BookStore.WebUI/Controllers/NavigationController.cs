using BookStore.BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class NavigationController : Controller
    {
        private IBookRepository _bookRepository;
        public NavigationController(IBookRepository repository) => _bookRepository = repository;

        public PartialViewResult Menu(string genre = null)
        {
            ViewBag.SelectedGenre = genre;

            IEnumerable<string> genres = _bookRepository.Books
                .Select(b => b.Genre)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
}