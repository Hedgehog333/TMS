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
    class CategoryDatabaseManagerSingleton
    {
        private CategoryDatabaseManagerSingleton()
        {}
        public static db.XMLCategoriesDB Instance
        {
            get
            {
                return dao.Manager<db.XMLCategoriesDB>.Instance;
            }
        }
    }
}