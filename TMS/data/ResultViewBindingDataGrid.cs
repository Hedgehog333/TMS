using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    public class ResultViewBindingDataGrid
    {
        public string TestName { get; set; }
        public string UserName { get; set; }
        public string UserLastname { get; set; }
        public string UserGroup { get; set; }
        public int correctQuestion { set; get; }
        public int totalQuestion { set; get; }
        public DateTime compliteTest { set; get; }
    }
}