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
    class XMLGroupDB : dao.IDAO<data.Group>
    {
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.Group get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            data.Group group = null;
            try
            {
                group = (from R in doc.Root.Elements("group")
                        where Int32.Parse(R.Attribute("id").Value) == id
                        select new data.Group
                            (
                               Int32.Parse(R.Attribute("id").Value),
                               R.Element("Name").Value
                            )).SingleOrDefault<data.Group>();
            }
            catch (Exception ex)
            { }
            return group;
        }
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.Group get(string name)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            data.Group group = null;
            try
            {
                group = (from R in doc.Root.Elements("group")
                         where R.Element("Name").Value.Equals(name)
                         select new data.Group
                             (
                                Int32.Parse(R.Attribute("id").Value),
                                R.Element("Name").Value
                             )).SingleOrDefault<data.Group>();
            }
            catch (Exception ex)
            { }
            return group;
        }
        public List<data.Group> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            var group = (from R in doc.Root.Elements("group")
                         select new data.Group
                             (
                                Int32.Parse(R.Attribute("id").Value),
                                R.Element("Name").Value
                             )).ToList<data.Group>();
            return group;
        }

        public void add(data.Group item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("group").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement group = new XElement("group",
                new XAttribute("id", ++maxId),
                new XElement("fName", item.Name));
            doc.Root.Add(group);
            doc.Save(ConfigurationManager.AppSettings["GroupsFile"]);
        }

        public void update(data.Group item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            var group = (from R in doc.Root.Elements("group")
                        where Int32.Parse(R.Attribute("id").Value) == item.id
                        select R).FirstOrDefault();
            group.SetElementValue("Name", item.Name);
            doc.Save(ConfigurationManager.AppSettings["GroupsFile"]);
        }

        public void delete(data.Group item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["GroupsFile"]);
            var group = doc.Root.Descendants("group").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            group.Remove();
            doc.Save(ConfigurationManager.AppSettings["GroupsFile"]);
        }

        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["GroupsFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "groups"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["GroupsFile"]);
            }
        }
    }
}