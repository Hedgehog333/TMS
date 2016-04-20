using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    class Question
    {
        public bool isFowAnswers { set; get; }
        public string body { set; get; }
        public List<data.Answer> answers;
        public Question()
        {
            this.answers = new List<Answer>();
        }
    }
}
