using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TMS.dao
{
    class XMLAnswerDB : IDAO<data.Answer>
    {
        public data.Answer get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["AnswersFile"]);
            var answer = (from A in doc.Root.Elements("answer")
                        where Int32.Parse(A.Attribute("id").Value) == id
                        select new data.Answer
                            (
                               Int32.Parse(A.Attribute("id").Value),
                               Boolean.Parse(A.Element("isTrue").Value),
                               A.Element("body").Value,
                               Int32.Parse(A.Attribute("questionId").Value)
                            )).SingleOrDefault<data.Answer>();
            return answer;
        }

        public List<data.Answer> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["AnswersFile"]);
            var answers = (from A in doc.Root.Elements("answer")
                          select new data.Answer
                              (
                                Int32.Parse(A.Attribute("id").Value),
                                Boolean.Parse(A.Element("isTrue").Value),
                                A.Element("body").Value,
                                Int32.Parse(A.Attribute("questionId").Value)
                              )).ToList<data.Answer>();
            return answers;
        }

        public void add(data.Answer item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["AnswersFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("answer").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement answer = new XElement("answer",
                new XAttribute("id", ++maxId),
                new XElement("isTrue", item.isTrue),
                new XElement("body", item.body),
                new XAttribute("questionId", item.questionId));
            doc.Root.Add(answer);
            doc.Save(ConfigurationManager.AppSettings["AnswersFile"]);
        }

        public void update(data.Answer item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["AnswersFile"]);
            var answer = (from A in doc.Root.Elements("answer")
                        where Int32.Parse(A.Attribute("id").Value) == item.id
                        select A).FirstOrDefault();
            answer.SetElementValue("isTrue", item.isTrue);
            answer.SetElementValue("body", item.body);
            answer.SetAttributeValue("questionId", item.questionId);
            doc.Save(ConfigurationManager.AppSettings["AnswersFile"]);
        }

        public void delete(data.Answer item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["AnswersFile"]);
            var answer = doc.Root.Descendants("answer").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            answer.Remove();
            doc.Save(ConfigurationManager.AppSettings["AnswersFile"]);
        }
        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["AnswersFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "answers"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["AnswersFile"]);
            }
        }
    }
}
