using BookStore.BusinessLogic.DataTransferObject;
using System.Collections.Generic;

namespace BookStore.BusinessLogic.Repository
{
    public interface IBookRepository
    {
        IEnumerable<BookDTO> Books { get; }
    }
}
