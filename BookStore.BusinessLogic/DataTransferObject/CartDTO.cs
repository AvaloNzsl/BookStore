using BookStore.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.DataTransferObject
{
    public class CartDTO
    {
        private List<CartLine> _lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get => _lineCollection;
        }

        public void AddItem(Book book, int quantity)
        {
            CartLine line = _lineCollection
                .Where(b => b.Book.BookId == book.BookId)
                .FirstOrDefault();

            if (line == null)
            {
                _lineCollection.Add(new CartLine { Book = book, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Book book) =>
            _lineCollection.RemoveAll(l => l.Book.BookId == book.BookId);

        public decimal TotalValue()
            => _lineCollection.Sum(x => x.Book.Price * x.Quantity);

        public void Clear() =>
            _lineCollection.Clear();

    }

    public class CartLine
    {
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
}
