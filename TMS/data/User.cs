using System;

namespace TMS.data
{
    public class User
    {
        int id { set; get; }
        string fName { set; get; }
        string lName { set; get; }
        string sName { set; get; }
        dao.ERoles role { set; get; }
        DateTime registrationDate { set; get; }
        DateTime lastOnlineDate { set; get; }
        
    }
}