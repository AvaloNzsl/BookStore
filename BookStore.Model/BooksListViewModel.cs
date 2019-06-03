using System.Collections.Generic;

namespace BookStore.Model
{
    public class BooksListViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageInfo PageInfo { get; set; }

        public string CurrentGenre { get; set; }
    }
}
