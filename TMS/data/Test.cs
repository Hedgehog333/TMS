using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.data
{
    class Test
    {
        public int id { set; get; }
        public string title { set; get; }
        public string desctiption { set; get; }
        public int categoriesId { set; get; }
        public DateTime creationDate { set; get; }
        public DateTime lastModefied { set; get; }
        public int authorId { set; get; }
        public bool isDraft { set; get; }
        //public List<data.Question> question = new List<Question>();
        public Test
            (
                int id,
                string title,
                string desctiption,
                int categoriesId,
                DateTime creationDate,
                DateTime lastModefied,
                int authorId,
                bool isDraft
            )
        {
            this.id = id;
            this.title = title;
            this.desctiption = desctiption;
            this.categoriesId = categoriesId;
            this.creationDate = creationDate;
            this.lastModefied = lastModefied;
            this.authorId = authorId;
            this.isDraft = isDraft;
            //this.question = new List<Question>();
        }
    }
}
