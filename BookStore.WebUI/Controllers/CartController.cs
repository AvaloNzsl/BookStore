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
        private IOrderProcessor _orderProcessor;
        public CartController(IBookRepository repository, IOrderProcessor processor)
        {
            _bookRepository = repository; _orderProcessor = processor;
        }

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

            var bookDB = _bookRepository.MapConfig(_bookDto);

            if (_bookDto != null)
            {
                cart.AddItem(bookDB, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
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

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(CartDTO cart, ShippingDetailsDTO shippingDetails)
        {
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Soory, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                _orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }

            else{
                return View(new ShippingDetails());
            }
        }
    }
}