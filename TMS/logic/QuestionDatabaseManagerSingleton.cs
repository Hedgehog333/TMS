using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.logic
{
    class QuestionDatabaseManagerSingleton
    {
        private QuestionDatabaseManagerSingleton()
        {}
        public static db.XMLQuestionDB Instance
        {
            get
            {
                return dao.Manager<db.XMLQuestionDB>.Instance;
            }
        }
    }
}