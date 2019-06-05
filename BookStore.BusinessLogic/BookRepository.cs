using AutoMapper;
using BookStore.BusinessLogic.DataTransferObject;
using BookStore.BusinessLogic.Repository;
using BookStore.DataAccessLayer;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BookStore.BusinessLogic
{
    public class BookRepository : IBookRepository
    {
        private Book _bookDB = new Book();
        EFBookStoreContext _bsContext = new EFBookStoreContext();

        public IEnumerable<BookDTO> Books
        {
            get
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Book>, List<BookDTO>>(_bsContext.Books);
            }
        }

        public Book MapConfig(BookDTO _bookDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            return _bookDB = mapper.Map<BookDTO, Book>(_bookDto);
        }
    }
}
