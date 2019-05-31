using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using BookStore.Model;

namespace BookStore.WebUI.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _bookRepository;

        public BooksController(IBookRepository repository) => _bookRepository = repository;

        public ViewResult List()
        {
            IEnumerable<BookDTO> booksDto = _bookRepository.Books;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var books = mapper.Map<IEnumerable<BookDTO>, List<Book>>(booksDto);

            return View(books);
        }
    }
}