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
        public bool isFowAnswers { set; get; }
        public string body { set; get; }
        public int teskId { set; get; }
        public List<data.Answer> answers;
        public Question
            (
                int id,
                bool isFowAnswers,
                string body,
                int teskId
            )
        {
            this.id = id;
            this.isFowAnswers = isFowAnswers;
            this.body = body;
            this.teskId = teskId;
            this.answers = new List<Answer>();
        }
    }
}
