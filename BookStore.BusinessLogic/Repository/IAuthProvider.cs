using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BusinessLogic.Repository
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string passowrd);
    }
}
