using BookStore.BusinessLogic.Repository;
using BookStore.Model;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthProvider _authProvider;
        public AccountController(IAuthProvider auth) => _authProvider = auth;

        public ViewResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AccountLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (_authProvider.Authenticate(model.UserName, model.Password))
                {
                    return Redirect(returnUrl ?? Url.Action("AdminPanel", "Admin"));
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                    return View();
                }
            }
            else return View();
        }
    }
}