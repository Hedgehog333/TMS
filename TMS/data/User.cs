using System;

namespace TMS.data
{
    public class User
    {
        public int id { set; get; }
        public string fName { set; get; }
        public string lName { set; get; }
        public string sName { set; get; }
        public dao.ERoles role { set; get; }
        public DateTime registrationDate { set; get; }
        public DateTime lastOnlineDate { set; get; }
        
    }
}