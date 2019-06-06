using AutoMapper;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IBookRepository _bookRepository;
        public AdminController(IBookRepository repository) => _bookRepository = repository;

        public ViewResult AdminPanel()
        {
            IEnumerable<BookDTO> booksDto = _bookRepository.Books;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var books = mapper.Map<IEnumerable<BookDTO>, List<Book>>(booksDto);

            return View(books);
        }

        public ViewResult Create()
        {
            return View("Edit", new Book());
        }

        public ViewResult Edit(int bookId)
        {
            BookDTO _bookDto = _bookRepository.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var bookModel = mapper.Map<BookDTO, Book>(_bookDto);

            return View(bookModel);
        }
        [HttpPost]
        public ActionResult Edit(BookDTO bookDto, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    bookDto.ImageMimeType = image.ContentType;
                    bookDto.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(bookDto.ImageData, 0, image.ContentLength);
                }
                _bookRepository.SaveBook(bookDto);
                TempData["message"] = string.Format("Changes about book information \"{0}\" saved", bookDto.Name);
                return RedirectToAction("AdminPanel");
            }
            else
            {
                return View(bookDto);
            }
        }

        [HttpPost]
        public ActionResult Delete(int bookId)
        {
            BookDTO deletedBook = _bookRepository.DeleteBook(bookId);
            if (deletedBook != null)
            {
                _bookRepository.Save();
                TempData["message"] = string.Format("Book \"{0}\" has been deleted",
                    deletedBook.Name);
            }
            return RedirectToAction("AdminPanel");
        }
    }
}