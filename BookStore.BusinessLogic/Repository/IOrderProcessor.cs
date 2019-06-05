using BookStore.BusinessLogic.DataTransferObject;

namespace BookStore.BusinessLogic.Repository
{
    public interface IOrderProcessor
    {
        void ProcessOrder(CartDTO cartDto, ShippingDetailsDTO shippingDetailsDto);
    }
}
