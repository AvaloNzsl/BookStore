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
        EFBookStoreContext _bsContext = new EFBookStoreContext();
        public IEnumerable<BookDTO> Books
        {
            get {
                var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Book>, List<BookDTO>>(_bsContext.Books);  }
        }
    }
}
