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
    class ResultDatabaseManagerSingleton
    {
        private ResultDatabaseManagerSingleton()
        {}
        public static db.XMLResultDB Instance
        {
            get
            {
                return dao.Manager<db.XMLResultDB>.Instance;
            }
        }
    }
}