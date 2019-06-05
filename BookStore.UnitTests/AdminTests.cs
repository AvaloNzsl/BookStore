using System.Collections.Generic;
using System.Linq;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.Model;
using BookStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace BookStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void IndexContainsAllBooks()
        {
            //arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                new BookDTO { BookId = 1, Name = "Book_1" },
                new BookDTO { BookId = 2, Name = "Book_2" },
                new BookDTO { BookId = 3, Name = "Book_3" },
                new BookDTO { BookId = 4, Name = "Book_4" },
                new BookDTO { BookId = 5, Name = "Book_5" },
                new BookDTO { BookId = 6, Name = "Book_6" },
                new BookDTO { BookId = 7, Name = "Book_7" }
            });

            AdminController controller = new AdminController(mock.Object);

            //act
            List<Book> books = ((IEnumerable<Book>)controller.AdminPanel().ViewData.Model).ToList();
            //assert
            Assert.AreEqual(books.Count(), 7);
            Assert.AreEqual(books[0].Name, "Book_1");
            Assert.AreEqual(books[1].Name, "Book_2");
        }

        [TestMethod]
        public void CanEditBook()
        {
            //arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                new BookDTO { BookId = 1, Name = "Book_1" },
                new BookDTO { BookId = 2, Name = "Book_2" },
                new BookDTO { BookId = 3, Name = "Book_3" },
                new BookDTO { BookId = 4, Name = "Book_4" },
                new BookDTO { BookId = 5, Name = "Book_5" },
                new BookDTO { BookId = 6, Name = "Book_6" },
                new BookDTO { BookId = 7, Name = "Book_7" }
            });

            AdminController controller = new AdminController(mock.Object);

            //act
            Book bookDto_1 = controller.Edit(1).ViewData.Model as Book;
            Book bookDto_2 = controller.Edit(2).ViewData.Model as Book;
            BookDTO result = controller.Edit(50).ViewData.Model as BookDTO;

            //assert
            Assert.AreEqual(1, bookDto_1.BookId);
            Assert.AreEqual(2, bookDto_2.BookId);
            Assert.IsNull(result);
        }
    }
}
