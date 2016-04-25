using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    class Task
    {
        public int id { set; get; }
        public string title { set; get; }
        public List<data.Question> question = new List<Question>();
        public Task()
        {
            this.question = new List<Question>();
        }
    }
}
