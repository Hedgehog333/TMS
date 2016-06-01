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
    class XMLResultDB : dao.IDAO<data.Result>
    {
        public data.Result get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["ResultsFile"]);
            var result = (from R in doc.Root.Elements("result")
                        where Int32.Parse(R.Attribute("id").Value) == id
                        select new data.Result
                            (
                               Int32.Parse(R.Attribute("id").Value),
                               Int32.Parse(R.Element("testId").Value),
                               Int32.Parse(R.Element("userId").Value),
                               Int32.Parse(R.Element("correctQuestion").Value),
                               Int32.Parse(R.Element("totalQuestion").Value),
                               DateTime.Parse(R.Element("compliteTest").Value)
                            )).SingleOrDefault<data.Result>();
            return result;
        }

        public List<data.Result> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["ResultsFile"]);
            var results = (from R in doc.Root.Elements("result")
                          select new data.Result
                              (
                                 Int32.Parse(R.Attribute("id").Value),
                                 Int32.Parse(R.Element("testId").Value),
                                 Int32.Parse(R.Element("userId").Value),
                                 Int32.Parse(R.Element("correctQuestion").Value),
                                 Int32.Parse(R.Element("totalQuestion").Value),
                                 DateTime.Parse(R.Element("compliteTest").Value)
                              )).ToList<data.Result>();
            return results;
        }

        public void add(data.Result item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["ResultsFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("result").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement result = new XElement("result",
                new XAttribute("id", ++maxId),
                new XElement("testId", item.testId),
                new XElement("userId", item.userId),
                new XElement("correctQuestion", item.correctQuestion),
                new XElement("totalQuestion", item.totalQuestion),
                new XElement("compliteTest", item.compliteTest));
            doc.Root.Add(result);
            doc.Save(ConfigurationManager.AppSettings["ResultsFile"]);
        }

        public void update(data.Result item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["ResultsFile"]);
            var test = (from A in doc.Root.Elements("result")
                        where Int32.Parse(A.Attribute("id").Value) == item.id
                        select A).FirstOrDefault();
            test.SetElementValue("testId", item.testId);
            test.SetElementValue("userId", item.userId);
            test.SetElementValue("correctQuestion", item.correctQuestion);
            test.SetElementValue("totalQuestion", item.totalQuestion);
            test.SetElementValue("compliteTest", item.compliteTest);
            doc.Save(ConfigurationManager.AppSettings["ResultsFile"]);
        }

        public void delete(data.Result item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["ResultsFile"]);
            var user = doc.Root.Descendants("result").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            user.Remove();
            doc.Save(ConfigurationManager.AppSettings["ResultsFile"]);
        }

        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["ResultsFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "results"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["ResultsFile"]);
            }
        }
    }
}
