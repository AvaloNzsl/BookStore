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
    }
}