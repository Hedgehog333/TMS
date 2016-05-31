using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.logic
{
    /// <summary>
    /// Needed to initialize the data CurrentUserSingleton.
    /// </summary>
    public class LoginMethod
    {
        private static  data.User _user;
        public static data.User getUser()
        {
            return _user;
        }
        public LoginMethod(string email)
        {
            _user = UserDatabaseManagerSingleton.Instance.get(email);
        }
    }
}
