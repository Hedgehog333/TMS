using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    class Question
    {
        public int id { set; get; }
        public string body { set; get; }
        public int testId { set; get; }
        public bool isFowAnswers { set; get; }
        public bool isDraft { set; get; }
        public List<data.Answer> answers;
        public Question
            (
                int id,
                string body,
                int testId,
                bool isFowAnswers,
                bool isDraft
            )
        {
            this.id = id;
            this.body = body;
            this.testId = testId;
            this.isFowAnswers = isFowAnswers;
            this.isDraft = isDraft;
            this.answers = new List<Answer>();
        }
    }
}
