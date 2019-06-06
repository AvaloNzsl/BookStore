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
        private BookDTO _bookDto = new BookDTO();
        EFBookStoreContext _bsContext = new EFBookStoreContext();

        public IEnumerable<BookDTO> Books
        {
            get
            {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Book>, List<BookDTO>>(_bsContext.Books);
            }
        }

        public void SaveBook(BookDTO book)
        {
            var bookDB = MapConfig(book);

            if (book.BookId == 0)
            {
                _bsContext.Books.Add(bookDB);
            }
            else
            {
                Book dbEntry = _bsContext.Books.Find(bookDB.BookId);
                if (dbEntry != null)
                {
                    dbEntry.Name = bookDB.Name;
                    dbEntry.Author = bookDB.Author;
                    dbEntry.Description = bookDB.Description;
                    dbEntry.Genre = bookDB.Genre;
                    dbEntry.Price = bookDB.Price;
                }
            }
            Save();
        }

        public BookDTO DeleteBook(int bookId)
        {
            Book deleteBook = _bsContext.Books.Find(bookId);
            if (deleteBook != null)
            {
                _bsContext.Books.Remove(deleteBook);
            }
            var bookDto = MapConfigReverse(deleteBook);

            return bookDto;
        }

        public Book MapConfig(BookDTO _bookDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            return _bookDB = mapper.Map<BookDTO, Book>(_bookDto);
        }
        public BookDTO MapConfigReverse(Book _book)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return _bookDto = mapper.Map<Book, BookDTO>(_book);
        }

        public void Save()
        {
            _bsContext.SaveChanges();
        }
    }
}
