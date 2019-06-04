
using BookStore.BusinessLogic.DataTransferObject;
using System.Web.Mvc;

namespace BookStore.WebUI.Infrastructure
{
    public class CartModelBinder : IModelBinder
    {
        private const string sessionKey = "Cart";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            CartDTO cart = null;
            if(controllerContext.HttpContext.Session != null)
            {
                cart = (CartDTO)controllerContext.HttpContext.Session[sessionKey];
            }

            if (cart == null)
            {
                cart = new CartDTO();
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session[sessionKey] = cart;
                }
            }

            return cart;
        }
    }
}