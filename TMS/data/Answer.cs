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
        public string body { set; get; }
        public bool isCorrect { set; get; }
        public int questionId { set; get; }
        public bool isDraft { set; get; }
        public Answer
            (
                int id,
                string body,
                bool isTrue,
                int questionId,
                bool isDraft
            )
        {
            this.id = id;
            this.body = body;
            this.isCorrect = isCorrect;
            this.questionId = questionId;
            this.isDraft = isDraft;
        }
    }
}
