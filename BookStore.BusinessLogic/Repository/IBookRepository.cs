using BookStore.BusinessLogic.DataTransferObject;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BookStore.BusinessLogic.Repository
{
    public interface IBookRepository
    {
        IEnumerable<BookDTO> Books { get; }
        BookDTO GetBookById(int bookId);
        void EditBook(BookDTO book);
        void AddBook(BookDTO book);
        void DeleteBook(int bookId);
        void Save();

        Book MapConfig(BookDTO _bookDto);
    }
}
