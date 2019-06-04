using BookStore.BusinessLogic.DataTransferObject;
using BookStore.WebUI.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(CartDTO), new CartModelBinder());
        }
    }
}
