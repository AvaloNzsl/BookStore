using BookStore.BusinessLogic.DataTransferObject;

namespace BookStore.Model
{
    public class CartViewModel
    {
        public CartDTO Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}
