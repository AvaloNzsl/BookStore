using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.Model;
using BookStore.WebUI.Controllers;
using BookStore.WebUI.HtmlHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BookStore.UnitTests
{
    [TestClass]
    public class BookStoreTest
    {
        [TestMethod]
        public void CanPageInt()
        {
            //имитация хранилища
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

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            BooksListViewModel result = (BooksListViewModel)controller.List(null, 2).Model;

            List<Book> books = result.Books.ToList();

            Assert.IsTrue(books.Count == 3);
            Assert.AreEqual(books[0].Name, "Book_4");
            Assert.AreEqual(books[1].Name, "Book_5");

        }

        [TestMethod]
        public void CanGeneratePageLinks()
        {
            //arrange
            HtmlHelper myHelper = null;
            PageInfo _pageInfo = new PageInfo()
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };
            Func<int, string> pageUrlDelegate = i => "Page" + i;
            //action
            MvcHtmlString result = myHelper.PageLinks(_pageInfo, pageUrlDelegate);
            //asserts
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                            + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                            + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                            result.ToString());
        }

        [TestMethod]
        public void CanSendPageViewModel()
        {
            // Arrange
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

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Action
            BooksListViewModel result = (BooksListViewModel)controller.List(null, 2).Model;

            // Asserts
            PageInfo _pageInfo = result.PageInfo;
            Assert.AreEqual(_pageInfo.CurrentPage, 2);
            Assert.AreEqual(_pageInfo.ItemsPerPage, 3);
            Assert.AreEqual(_pageInfo.TotalItems, 7);
            Assert.AreEqual(_pageInfo.TotalPages, 3);
        }

        [TestMethod]
        public void CanFilterBooks()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                    new BookDTO { BookId = 1, Name = "Book_1" , Genre = "Genre1"},
                    new BookDTO { BookId = 2, Name = "Book_2" , Genre = "Genre2"},
                    new BookDTO { BookId = 3, Name = "Book_3" , Genre = "Genre3"},
                    new BookDTO { BookId = 4, Name = "Book_4" , Genre = "Genre4"},
                    new BookDTO { BookId = 5, Name = "Book_5" , Genre = "Genre5"},
                    new BookDTO { BookId = 6, Name = "Book_6" , Genre = "Genre6"},
                    new BookDTO { BookId = 7, Name = "Book_7" , Genre = "Genre2"}
                });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Action
            List<Book> result = ((BooksListViewModel)controller.List("Genre2", 1).Model).Books.ToList();

            // Asserts
            Assert.AreEqual(result.Count, 2);
            Assert.IsTrue(result[0].Name == "Book_2" && result[0].Genre == "Genre2");
            Assert.IsTrue(result[1].Name == "Book_7" && result[0].Genre == "Genre2");
        }

        [TestMethod]
        public void CanCreateCategories()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                    new BookDTO { BookId = 1, Name = "Book_1" , Genre = "Genre1"},
                    new BookDTO { BookId = 2, Name = "Book_2" , Genre = "Genre2"},
                    new BookDTO { BookId = 3, Name = "Book_3" , Genre = "Genre3"},
                    new BookDTO { BookId = 4, Name = "Book_4" , Genre = "Genre1"},
                    new BookDTO { BookId = 5, Name = "Book_5" , Genre = "Genre3"},
                    new BookDTO { BookId = 6, Name = "Book_6" , Genre = "Genre1"},
                    new BookDTO { BookId = 7, Name = "Book_7" , Genre = "Genre2"}
                });

            NavigationController target = new NavigationController(mock.Object);

            // Action
            List<string> result = ((IEnumerable<string>)target.Menu().Model).ToList();

            // Asserts
            Assert.AreEqual(result.Count, 3);
            Assert.AreEqual(result[0], "Genre1");
            Assert.AreEqual(result[1], "Genre2");
            Assert.AreEqual(result[2], "Genre3");

        }


        [TestMethod]
        public void IndicatesSelectedGenre()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                    new BookDTO { BookId = 1, Name = "Book_1" , Genre = "Genre1"},
                    new BookDTO { BookId = 2, Name = "Book_2" , Genre = "Genre2"},
                    new BookDTO { BookId = 3, Name = "Book_3" , Genre = "Genre3"},
                    new BookDTO { BookId = 4, Name = "Book_4" , Genre = "Genre1"},
                    new BookDTO { BookId = 5, Name = "Book_5" , Genre = "Genre3"},
                    new BookDTO { BookId = 6, Name = "Book_6" , Genre = "Genre1"},
                    new BookDTO { BookId = 7, Name = "Book_7" , Genre = "Genre2"}
                });

            NavigationController target = new NavigationController(mock.Object);

            string genreToSelect = "Genre2";

            // Action
            string result = target.Menu(genreToSelect).ViewBag.SelectedGenre;

            // Asserts
            Assert.AreEqual(genreToSelect, result);

        }

        [TestMethod]
        public void GenerateGenreSpecificBookCount()
        {
            // Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new List<BookDTO> {
                    new BookDTO { BookId = 1, Name = "Book_1" , Genre = "Genre1"},
                    new BookDTO { BookId = 2, Name = "Book_2" , Genre = "Genre2"},
                    new BookDTO { BookId = 3, Name = "Book_3" , Genre = "Genre3"},
                    new BookDTO { BookId = 4, Name = "Book_4" , Genre = "Genre1"},
                    new BookDTO { BookId = 5, Name = "Book_5" , Genre = "Genre3"},
                    new BookDTO { BookId = 6, Name = "Book_6" , Genre = "Genre1"},
                    new BookDTO { BookId = 7, Name = "Book_7" , Genre = "Genre2"}
                });

            BooksController controller = new BooksController(mock.Object);
            controller.pageSize = 3;

            // Action
            int res_1 = ((BooksListViewModel)controller.List("Genre1").Model).PageInfo.TotalItems;
            int res_2 = ((BooksListViewModel)controller.List("Genre2").Model).PageInfo.TotalItems;
            int res_3 = ((BooksListViewModel)controller.List("Genre3").Model).PageInfo.TotalItems;
            int res_All = ((BooksListViewModel)controller.List(null).Model).PageInfo.TotalItems;

            // Asserts
            Assert.AreEqual(res_1, 3);
            Assert.AreEqual(res_2, 2);
            Assert.AreEqual(res_3, 2);
            Assert.AreEqual(res_All, 7);

        }
    }
}
