using BookStore.BusinessLogic.Repository;
using System.Web.Security;

namespace BookStore.BusinessLogic
{
    public class FormAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string passowrd)
        {
            bool result = FormsAuthentication.Authenticate(username, passowrd);
            if (result)
            {
                FormsAuthentication.SetAuthCookie(username, false);
            }
            return result;
        }
    }
}
