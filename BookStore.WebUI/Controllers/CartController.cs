using AutoMapper;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.DataAccessLayer.Entities;
using BookStore.Model;
using System.Linq;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository _bookRepository;
        public CartController(IBookRepository repository) => _bookRepository = repository;

        public ViewResult Index(CartDTO cart, string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(CartDTO cart, int bookId, string returnUrl)
        {
            BookDTO _bookDto = _bookRepository.Books
                .FirstOrDefault(b => b.BookId == bookId);

            var bookDB =_bookRepository.MapConfig(_bookDto);

            if (_bookDto != null)
            {
                cart.AddItem(bookDB, 1);
            }

            return RedirectToAction("Index",new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(CartDTO cart, int bookId, string returnUrl)
        {
            BookDTO _bookDto = _bookRepository.Books
                .FirstOrDefault(b => b.BookId == bookId);
            var bookDB = _bookRepository.MapConfig(_bookDto);

            if (_bookDto != null)
            {
                cart.RemoveLine(bookDB);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(CartDTO cart)
        {
            return PartialView(cart);
        }
    }
}