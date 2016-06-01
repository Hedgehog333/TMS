using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.logic
{
    /// <summary>
    /// There is determined a database to work.
    /// </summary>
    public class UserDatabaseManagerSingleton
    {
        private UserDatabaseManagerSingleton()
        {}
        public static db.XMLUserDB Instance
        {
            get
            {
                return dao.Manager<db.XMLUserDB>.Instance;
            }
        }
    }
}
