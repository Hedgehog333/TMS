using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace TMS.db
{
    class XMLQuestionDB : dao.IDAO<data.Question>
    {
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.Question get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["QuestionsFile"]);
            data.Question question = null;
            try
            {
                question = (from Q in doc.Root.Elements("question")
                          where Int32.Parse(Q.Attribute("id").Value) == id
                          select new data.Question
                              (
                                 Int32.Parse(Q.Attribute("id").Value),
                                 Q.Element("body").Value,
                                 Int32.Parse(Q.Attribute("testId").Value),
                                 Boolean.Parse(Q.Element("isFowAnswers").Value),
                                 Boolean.Parse(Q.Element("isDraft").Value)
                              )).SingleOrDefault<data.Question>();
            }
            catch (Exception ex)
            { }
            return question;
        }
        public List<data.Question> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["QuestionsFile"]);
            var questions = (from Q in doc.Root.Elements("question")
                            select new data.Question
                                (
                                   Int32.Parse(Q.Attribute("id").Value),
                                   Q.Element("body").Value,
                                   Int32.Parse(Q.Attribute("testId").Value),
                                   Boolean.Parse(Q.Element("isFowAnswers").Value),
                                   Boolean.Parse(Q.Element("isDraft").Value)
                                )).ToList<data.Question>();
            return questions;
        }

        public void add(data.Question item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["QuestionsFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("question").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement question = new XElement("question",
                new XAttribute("id", ++maxId),
                new XElement("body", item.body),
                new XAttribute("testId", item.testId),
                new XElement("isFowAnswers", item.isFowAnswers),
                new XElement("isDraft", item.isDraft));
            doc.Root.Add(question);
            doc.Save(ConfigurationManager.AppSettings["QuestionsFile"]);
        }

        public void update(data.Question item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["QuestionsFile"]);
            var question = (from Q in doc.Root.Elements("question")
                          where Int32.Parse(Q.Attribute("id").Value) == item.id
                          select Q).FirstOrDefault();
            question.SetElementValue("body", item.body);
            question.SetAttributeValue("testId", item.testId);
            question.SetElementValue("isFowAnswers", item.isFowAnswers);
            question.SetElementValue("isDraft", item.isDraft);
            doc.Save(ConfigurationManager.AppSettings["QuestionsFile"]);
        }

        public void delete(data.Question item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["QuestionsFile"]);
            var question = doc.Root.Descendants("question").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            question.Remove();
            doc.Save(ConfigurationManager.AppSettings["QuestionsFile"]);
        }
        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["QuestionsFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "questions"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["QuestionsFile"]);
            }
        }
    }
}
