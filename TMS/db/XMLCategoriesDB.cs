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
    class XMLCategoriesDB : dao.IDAO<data.Categories>
    {
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.Categories get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            data.Categories category = null;
            try
            {
                category = (from C in doc.Root.Elements("category")
                            where Int32.Parse(C.Attribute("id").Value) == id
                              select new data.Categories
                                  (
                                    Int32.Parse(C.Attribute("id").Value),
                                    C.Element("title").Value
                                  )).SingleOrDefault<data.Categories>();
            }
            catch (Exception ex)
            { }
            return category;
        }
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.Categories get(string name)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            data.Categories category = null;
            try
            {
                category = (from R in doc.Root.Elements("category")
                            where R.Element("title").Value.Equals(name)
                            select new data.Categories
                             (
                                Int32.Parse(R.Attribute("id").Value),
                                R.Element("title").Value
                             )).SingleOrDefault<data.Categories>();
            }
            catch (Exception ex)
            { }
            return category;
        }
        public List<data.Categories> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            var categories = (from C in doc.Root.Elements("category")
                           select new data.Categories
                               (
                                 Int32.Parse(C.Attribute("id").Value),
                                 C.Element("title").Value
                               )).ToList<data.Categories>();
            return categories;
        }

        public void add(data.Categories item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("category").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception) { maxId = 0; }

            XElement category = new XElement("category",
                new XAttribute("id", ++maxId),
                new XElement("title", item.title));
            doc.Root.Add(category);
            doc.Save(ConfigurationManager.AppSettings["CategoriesFile"]);
        }

        public void update(data.Categories item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            var category = (from C in doc.Root.Elements("category")
                          where Int32.Parse(C.Attribute("id").Value) == item.id
                          select C).FirstOrDefault();
            category.SetElementValue("title", item.title);
            doc.Save(ConfigurationManager.AppSettings["CategoriesFile"]);
        }

        public void delete(data.Categories item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["CategoriesFile"]);
            var category = doc.Root.Descendants("category").Where(
                   t => Int32.Parse(t.Attribute("id").Value) == item.id
                ).ToList();
            category.Remove();
            doc.Save(ConfigurationManager.AppSettings["CategoriesFile"]);
        }

        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["CategoriesFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "categories"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["CategoriesFile"]);
            }
        }
    }
}
