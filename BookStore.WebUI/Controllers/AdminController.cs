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

        public ViewResult Edit(int bookId)
        {
            BookDTO _bookDto = _bookRepository.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var _bookDB = mapper.Map<BookDTO, Book>(_bookDto);

            return View(_bookDB);
        }
        [HttpPost]
        public ActionResult Edit(BookDTO bookDto)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.EditBook(bookDto);
                _bookRepository.Save();
                TempData["message"] = string.Format("Changes about book information \"{0}\" saved", bookDto.Name);
                return RedirectToAction("AdminPanel");
            }
            else
            {
                return View(bookDto);
            }
        }
    }
}