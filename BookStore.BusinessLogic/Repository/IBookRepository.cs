using BookStore.BusinessLogic.DataTransferObject;
using BookStore.DataAccessLayer.Entities;
using System.Collections.Generic;

namespace BookStore.BusinessLogic.Repository
{
    public interface IBookRepository
    {
        IEnumerable<BookDTO> Books { get; }

    }
}
