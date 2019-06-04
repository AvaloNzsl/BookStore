using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.DataAccessLayer.Entities;
using BookStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookStore.UnitTests
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            // Arrange
            Book book_1 = new Book { BookId = 1, Name = "Book_1" };
            Book book_2 = new Book { BookId = 2, Name = "Book_2" };

            CartDTO cart = new CartDTO();

            //Action
            cart.AddItem(book_1, 1);
            cart.AddItem(book_2, 1);
            List<BusinessLogic.DataTransferObject.CartLine> result = cart.Lines.ToList();

            //Asserts
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Book, book_1);
            Assert.AreEqual(result[1].Book, book_2);
        }

        [TestMethod]
        public void CanAddQuantityForExistingLines()
        {
            // Arrange
            Book book_1 = new Book { BookId = 1, Name = "Book_1" };
            Book book_2 = new Book { BookId = 2, Name = "Book_2" };

            CartDTO cart = new CartDTO();

            //Action
            cart.AddItem(book_1, 1);
            cart.AddItem(book_2, 1);
            cart.AddItem(book_1, 5);
            List<CartLine> result = cart.Lines.OrderBy(c => c.Book.BookId).ToList();

            //Asserts
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(result[0].Quantity, 6);
            Assert.AreEqual(result[1].Quantity, 1);
        }

        [TestMethod]
        public void CanRemoveLine()
        {
            // Arrange
            Book book_1 = new Book { BookId = 1, Name = "Book_1" };
            Book book_2 = new Book { BookId = 2, Name = "Book_2" };
            Book book_3 = new Book { BookId = 3, Name = "Book_3" };
            Book book_4 = new Book { BookId = 4, Name = "Book_4" };
            Book book_5 = new Book { BookId = 5, Name = "Book_5" };

            CartDTO cart = new CartDTO();

            //Action
            cart.AddItem(book_1, 1);
            cart.AddItem(book_2, 1);
            cart.AddItem(book_4, 5);
            cart.AddItem(book_5, 5);
            cart.RemoveLine(book_4);

            //Asserts
            Assert.AreEqual(cart.Lines.Where(c => c.Book == book_4).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 3);
        }

        [TestMethod]
        public void CalculateCartTotalRemoveAndCalculate()
        {
            // Arrange
            Book book_1 = new Book { BookId = 1, Name = "Book_1", Price = 150 };
            Book book_2 = new Book { BookId = 2, Name = "Book_2", Price = 250 };
            Book book_3 = new Book { BookId = 3, Name = "Book_3", Price = 200 };
            Book book_4 = new Book { BookId = 4, Name = "Book_4", Price = 50 };
            Book book_5 = new Book { BookId = 5, Name = "Book_5", Price = 50 };

            CartDTO cart = new CartDTO();

            //Action
            cart.AddItem(book_1, 1);
            cart.AddItem(book_2, 1);
            cart.AddItem(book_4, 1);
            cart.AddItem(book_5, 1);

            //Asserts
            decimal result = cart.TotalValue();
            Assert.AreEqual(result, 500);

            //Action
            cart.RemoveLine(book_4);
            result = cart.TotalValue();

            //Asserts
            Assert.AreEqual(cart.Lines.Where(c => c.Book == book_4).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 3);

            Assert.AreEqual(result, 450);
        }

        //public void CanClearLines()
        //{
        //    // Arrange
        //    Book book_1 = new Book { BookId = 1, Name = "Book_1" };
        //    Book book_2 = new Book { BookId = 2, Name = "Book_2" };
        //    Book book_3 = new Book { BookId = 3, Name = "Book_3" };
        //    Book book_4 = new Book { BookId = 4, Name = "Book_4" };
        //    Book book_5 = new Book { BookId = 5, Name = "Book_5" };

        //    CartDTO cart = new CartDTO();

        //    //Action
        //    cart.AddItem(book_1, 1);
        //    cart.AddItem(book_2, 1);
        //    cart.AddItem(book_4, 5);
        //    cart.AddItem(book_5, 5);
        //    cart.Clear();

        //    //Asserts
        //    Assert.AreEqual(cart.Lines.Count(), 0);
        //}

        [TestMethod]
        public void CanAddToCart()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            IEnumerable<Book> _bookDto = null;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            var _bookDB = mapper.Map<IEnumerable<Book>, List<BookDTO>>(_bookDto);

            _bookDB = new List<BookDTO>
            {
                new BookDTO{ BookId =1, Name ="Book_1", Genre ="Genre_1"}
            };

            mock.Setup(m => m.Books).Returns(_bookDB.AsQueryable());

            CartDTO cart = new CartDTO();
            CartController controller = new CartController(mock.Object);

            //Action
            controller.AddToCart(cart, 1, null);

            ////Asserts
            //Assert.AreEqual(cart.Lines.Count(), 1);
            //Assert.AreEqual(cart.Lines.ToList()[0].Book.BookId, 1);

        }

        [TestMethod]
        public void AddingBookToCartGoesToCartScreen()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO>
            {
                new BookDTO {BookId =1, Name ="Book_1", Genre="Genre_1"}
            }.AsQueryable());

            CartDTO cart = new CartDTO();
            CartController controller = new CartController(mock.Object);

            //Action
            RedirectToRouteResult result = controller.AddToCart(cart, 2, "myUrl");

            //Asserts
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "myUrl");

        }

        [TestMethod]
        public void CanViewCartContents()
        {
            // Arrange
            CartDTO cart = new CartDTO();
            CartController controller = new CartController(null);

            //Action
            Model.CartViewModel result = (Model.CartViewModel)controller.Index(cart, "myUrl").ViewData.Model;

            //Asserts
            Assert.AreEqual(result.Cart, cart);
            Assert.AreEqual(result.ReturnUrl, "myUrl");

        }

    }
}
