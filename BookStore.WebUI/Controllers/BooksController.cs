using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using System;
using System.Collections.Generic;
using AutoMapper;
using System.Web.Mvc;
using BookStore.Model;
using System.Linq;

namespace BookStore.WebUI.Controllers
{
    public class BooksController : Controller
    {
        private IBookRepository _bookRepository;
        //
        public int pageSize = 3;

        public BooksController(IBookRepository repository) => _bookRepository = repository;

        public ViewResult List(int page = 1)
        {
            IEnumerable<BookDTO> booksDto = _bookRepository.Books;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var books = mapper.Map<IEnumerable<BookDTO>, List<Book>>(booksDto);

            BooksListViewModel model = new BooksListViewModel
            {
                Books = books.OrderBy( b => b.BookId )
                .Skip((page -1)*pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _bookRepository.Books.Count()
                }                
            };

            return View(model);
        }


    }
}