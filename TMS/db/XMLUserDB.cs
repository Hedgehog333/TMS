using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.IO;
using System.Xml;

namespace TMS.db
{
    public class XMLUserDB : dao.IDAO<data.User>
    {
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.User get(int id)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            data.User user = null;
            try
            {
                user = (from U in doc.Root.Elements("user")
                             where Int32.Parse(U.Attribute("id").Value) == id
                             select new data.User
                                 (
                                    Int32.Parse(U.Attribute("id").Value),
                                    U.Element("fName").Value,
                                    U.Element("lName").Value,
                                    U.Element("sName").Value,
                                    U.Element("email").Value,
                                    U.Element("password").Value,
                                    (data.ERoles)Int32.Parse(U.Element("role").Value),
                                    DateTime.Parse(U.Element("registrationDate").Value),
                                    DateTime.Parse(U.Element("lastOnlineDate").Value)
                                 )).SingleOrDefault<data.User>();
            }
            catch (Exception ex)
            { }
            return user;
        }
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.User get(string email)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            data.User user = null;
            try
            {
                user = (from U in doc.Root.Elements("user")
                            where U.Element("email").Value.Equals(email)
                            select new data.User
                                (
                                   Int32.Parse(U.Attribute("id").Value),
                                   U.Element("fName").Value,
                                   U.Element("lName").Value,
                                   U.Element("sName").Value,
                                   U.Element("email").Value,
                                   U.Element("password").Value,
                                   (data.ERoles)Int32.Parse(U.Element("role").Value),
                                   DateTime.Parse(U.Element("registrationDate").Value),
                                   DateTime.Parse(U.Element("lastOnlineDate").Value)
                                )).SingleOrDefault<data.User>();
            }
            catch(Exception ex)
            {}
            return user;
            
        }
        /// <summary>
        /// return null if User not found
        /// </summary>
        public data.User get(string rowName, string value)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            data.User user = null;
            try
            {
                user = (from U in doc.Root.Elements("user")
                        where U.Element(rowName).Value.Equals(value)
                        select new data.User
                            (
                               Int32.Parse(U.Attribute("id").Value),
                               U.Element("fName").Value,
                               U.Element("lName").Value,
                               U.Element("sName").Value,
                               U.Element("email").Value,
                               U.Element("password").Value,
                               (data.ERoles)Int32.Parse(U.Element("role").Value),
                               DateTime.Parse(U.Element("registrationDate").Value),
                               DateTime.Parse(U.Element("lastOnlineDate").Value)
                            )).SingleOrDefault<data.User>();
            }
            catch (Exception ex)
            { }
            return user;
        }
        public List<data.User> getAll()
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            List<data.User> users = (from U in doc.Root.Elements("user")
                        select new data.User
                            (
                                Int32.Parse(U.Attribute("id").Value),
                                U.Element("fName").Value,
                                U.Element("lName").Value,
                                U.Element("sName").Value,
                                U.Element("email").Value,
                                U.Element("password").Value,
                                (data.ERoles)Int32.Parse(U.Element("role").Value),
                                DateTime.Parse(U.Element("registrationDate").Value),
                                DateTime.Parse(U.Element("lastOnlineDate").Value)
                            )).ToList<data.User>();
            return users;
        }

        public void add(data.User item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            int maxId;
            try
            {
                maxId = doc.Root.Elements("user").Max(t => Int32.Parse(t.Attribute("id").Value));
            }
            catch (Exception){ maxId = 0; }
            
            XElement user = new XElement("user",
                new XAttribute("id", ++maxId),
                new XElement("fName", item.fName),
                new XElement("lName", item.lName),
                new XElement("sName", item.sName),
                new XElement("email", item.email),
                new XElement("password", item.password),
                new XElement("role", (int)item.role),
                new XElement("registrationDate", item.registrationDate),
                new XElement("lastOnlineDate", item.lastOnlineDate));
            doc.Root.Add(user);
            doc.Save(ConfigurationManager.AppSettings["UsersFile"]);
        }

        public void update(data.User item)
        {
            IsFileExists();

            XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
            var user = (from U in doc.Root.Elements("user")
                        where Int32.Parse(U.Attribute("id").Value) == item.id
                        select U).FirstOrDefault();
            user.SetElementValue("fName", item.fName);
            user.SetElementValue("lName", item.lName);
            user.SetElementValue("sName", item.sName);
            user.SetElementValue("email", item.email);
            user.SetElementValue("password", item.password);
            user.SetElementValue("role", (int)item.role);
            user.SetElementValue("registrationDate", item.registrationDate);
            user.SetElementValue("lastOnlineDate", item.lastOnlineDate);
            doc.Save(ConfigurationManager.AppSettings["UsersFile"]);
        }

        public void delete(data.User item)
        {
            IsFileExists();

             XDocument doc = XDocument.Load(ConfigurationManager.AppSettings["UsersFile"]);
             var user = doc.Root.Descendants("user").Where(
                    t=> Int32.Parse(t.Attribute("id").Value) == item.id
                 ).ToList();
             user.Remove();
             doc.Save(ConfigurationManager.AppSettings["UsersFile"]);
        }

        static void IsFileExists()
        {
            if (!File.Exists(ConfigurationManager.AppSettings["UsersFile"]))
            {
                XNamespace empNM = "urn:lst-emp:emp";

                XDocument xDoc = new XDocument(
                        new XDeclaration("1.0", "UTF-16", null),
                        new XElement(empNM + "users"
                                )
                        );
                StringWriter sw = new StringWriter();
                XmlWriter xWrite = XmlWriter.Create(sw);
                xDoc.Save(xWrite);
                xWrite.Close();
                xDoc.Save(ConfigurationManager.AppSettings["UsersFile"]);
            }
        }

    }
}
