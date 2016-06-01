using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    public class Group
    {
        public int id { private set; get; }
        public string Name { set; get; }
        public Group
            (
                int id,
                string Name
            )
        {
            this.id = id;
            this.Name = Name;
        }
        public override string ToString()
        {
            return "id: " + this.id +
                "\nName: " + this.Name;
        }
    }
}
