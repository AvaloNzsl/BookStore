using BookStore.DataAccessLayer.Entities;
using System.Data.Entity;

namespace BookStore.DataAccessLayer
{
    public class EFBookStoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
