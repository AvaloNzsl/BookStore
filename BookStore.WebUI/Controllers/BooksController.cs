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

        public ViewResult List(string genre, int page = 1)
        {
            IEnumerable<BookDTO> booksDto = _bookRepository.Books;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var books = mapper.Map<IEnumerable<BookDTO>, List<Book>>(booksDto);

            BooksListViewModel model = new BooksListViewModel
            {
                Books = books
                .Where( b=> genre == null || b.Genre == genre)
                .OrderBy( b => b.BookId )
                .Skip((page -1)*pageSize)
                .Take(pageSize),

                PageInfo = new PageInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = genre == null ?                                            //If -> there is selected category
                        _bookRepository.Books.Count() :                                     //return -> the number of items in this category(genre)
                        _bookRepository.Books.Where(book => book.Genre == genre).Count()    //else -> the total quantity of goods
                },
                CurrentGenre = genre
            };

            return View(model);
        }




        public FileContentResult GetImage(int bookId)
        {
            BookDTO _bookDto = _bookRepository.Books.Where(b => b.BookId == bookId).FirstOrDefault();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            var bookModel = mapper.Map<BookDTO, Book>(_bookDto);

            if (bookModel != null)
            {
                return File(bookModel.ImageData, bookModel.ImageMimeType);
            }
            else
            {
                return null;
            }
        }


    }
}