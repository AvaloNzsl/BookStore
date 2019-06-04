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

        public ViewResult Index(string returnUrl)
        {
            return View(new CartViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public CartDTO GetCart()
        {
            CartDTO cart = (CartDTO)Session["Cart"];
            if (cart == null)
            {
                cart = new CartDTO();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public RedirectToRouteResult AddToCart(int bookId, string returnUrl)
        {
            BookDTO _bookDto = _bookRepository.Books
                .FirstOrDefault(b => b.BookId == bookId);

            var bookDB =_bookRepository.MapConfig(_bookDto);

            if (_bookDto != null)
            {
                GetCart().AddItem(bookDB, 1);
            }

            return RedirectToAction("Index",new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int bookId, string returnUrl)
        {
            BookDTO _bookDto = _bookRepository.Books
                .FirstOrDefault(b => b.BookId == bookId);
            var bookDB = _bookRepository.MapConfig(_bookDto);

            if (_bookDto != null)
            {
                GetCart().RemoveLine(bookDB);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
    }
}