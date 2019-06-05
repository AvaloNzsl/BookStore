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

        public void AddBook(BookDTO book)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteBook(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public void EditBook(BookDTO book)
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
        }

        public BookDTO GetBookById(int bookId)
        {
            throw new System.NotImplementedException();
        }

        public Book MapConfig(BookDTO _bookDto)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<BookDTO, Book>()).CreateMapper();
            return _bookDB = mapper.Map<BookDTO, Book>(_bookDto);
        }

        public void Save()
        {
            _bsContext.SaveChanges();
        }
    }
}
