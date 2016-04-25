using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    class Answer
    {
        public int id { set; get; }
        public bool isTrue { set; get; }
        public string body { set; get; }
        public int questionId { set; get; }
        public Answer
            (
                int id,
                bool isTrue,
                string body,
                int questionId
            )
        {
            this.id = id;
            this.isTrue = isTrue;
            this.body = body;
            this.questionId = questionId;
        }
    }
}
