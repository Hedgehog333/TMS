using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    public class Categories
    {
        public int id { private set; get; }
        public string title { set; get; }

        public Categories
            (
                int id,
                string title
            )
        {
            this.id = id;
            this.title = title;
        }
    }
}
