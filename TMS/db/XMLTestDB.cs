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
    class XMLTestDB : dao.IDAO<data.Test>
    {
        public data.Test get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            var task = (from T in doc.Root.Elements("test")
                          where Int32.Parse(T.Attribute("id").Value) == id
                          select new data.Test
                              (
                                 Int32.Parse(T.Attribute("id").Value),
                                 T.Element("title").Value,
                                 T.Element("desctiption").Value,
                                 Int32.Parse(T.Attribute("categoriesId").Value),
                                 DateTime.Parse(T.Element("creationDate").Value),
                                 DateTime.Parse(T.Element("lastModefied").Value),
                                 Int32.Parse(T.Attribute("authorId").Value),
                                 Boolean.Parse(T.Element("isDraft").Value)
                              )).SingleOrDefault<data.Test>();
            return task;
        }
        public data.Test get(string param, string value)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            var task = (from T in doc.Root.Elements("test")
                        where T.Attribute(param).Value.Equals(value)
                        select new data.Test
                            (
                               Int32.Parse(T.Attribute("id").Value),
                               T.Element("title").Value,
                               T.Element("desctiption").Value,
                               Int32.Parse(T.Attribute("categoriesId").Value),
                               DateTime.Parse(T.Element("creationDate").Value),
                               DateTime.Parse(T.Element("lastModefied").Value),
                               Int32.Parse(T.Attribute("authorId").Value),
                               Boolean.Parse(T.Element("isDraft").Value)
                            )).SingleOrDefault<data.Test>();
            return task;
        }

        public List<data.Test> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            var tasks = (from T in doc.Root.Elements("test")
                        select new data.Test
                            (
                               Int32.Parse(T.Attribute("id").Value),
                                 T.Element("title").Value,
                                 T.Element("desctiption").Value,
                                 Int32.Parse(T.Attribute("categoriesId").Value),
                                 DateTime.Parse(T.Element("creationDate").Value),
                                 DateTime.Parse(T.Element("lastModefied").Value),
                                 Int32.Parse(T.Attribute("authorId").Value),
                                 Boolean.Parse(T.Element("isDraft").Value)
                            )).ToList<data.Test>();
            return tasks;
        }

        public void add(data.Test item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("test").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement tests = new XElement("test",
                new XAttribute("id", ++maxId),
                new XElement("title", item.title),
                new XElement("desctiption", item.desctiption),
                new XAttribute("categoriesId", item.categoriesId),
                new XElement("creationDate", item.creationDate),
                new XElement("lastModefied", item.lastModefied),
                new XAttribute("authorId", item.authorId),
                new XElement("isDraft", item.isDraft));
            doc.Root.Add(tests);
            doc.Save(ConfigurationManager.AppSettings["TestsFile"]);
        }

        public void update(data.Test item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            var test = (from A in doc.Root.Elements("test")
                          where Int32.Parse(A.Attribute("id").Value) == item.id
                          select A).FirstOrDefault();
            test.SetElementValue("title", item.title);
            test.SetElementValue("desctiption", item.desctiption);
            test.SetAttributeValue("categoriesId", item.categoriesId);
            test.SetElementValue("creationDate", item.creationDate);
            test.SetElementValue("lastModefied", item.lastModefied);
            test.SetAttributeValue("authorId", item.authorId);
            test.SetElementValue("isDraft", item.isDraft);
            doc.Save(ConfigurationManager.AppSettings["TestsFile"]);
        }

        public void delete(data.Test item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["TestsFile"]);
            var user = doc.Root.Descendants("test").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            user.Remove();
            doc.Save(ConfigurationManager.AppSettings["TestsFile"]);
        }
        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["TestsFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "tests"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["TestsFile"]);
            }
        }
    }
}
