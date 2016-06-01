using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    public class Result
    {
        public int id { set; get; }
        public int testId { set; get; }
        public int userId { set; get; }
        public int correctQuestion { set; get; }
        public int totalQuestion { set; get; }
        public DateTime compliteTest { set; get; }
        public Result
            (
                int id,
                int testId,
                int userId,
                int correctQuestion,
                int totalQuestion,
                DateTime compliteTest
            )
        {
            this.id = id;
            this.testId = testId;
            this.userId = userId;
            this.correctQuestion = correctQuestion;
            this.totalQuestion = totalQuestion;
            this.compliteTest = compliteTest;
        }
        public override string ToString()
        {
            return "id: " + this.id +
                "\nTestId: " + this.testId +
                "\nUserId: " + this.userId +
                "\nCorrectQuestion: " + this.correctQuestion +
                "\nTotalQuestion: " + this.totalQuestion +
                "\nCompliteTest: " + this.compliteTest;
        }
    }
}
