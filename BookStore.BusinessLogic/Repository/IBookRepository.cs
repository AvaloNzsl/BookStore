using BookStore.BusinessLogic.DataTransferObject;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BookStore.BusinessLogic.Repository
{
    public interface IBookRepository
    {
        IEnumerable<BookDTO> Books { get; }

        void SaveBook(BookDTO book);
        BookDTO DeleteBook(int bookId);
        void Save();

        Book MapConfig(BookDTO _bookDto);
        BookDTO MapConfigReverse(Book _book);
    }
}
