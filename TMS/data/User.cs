﻿using System;

namespace TMS.data
{
    public class User
    {
        public int id { private set; get; }
        public string fName { set; get; }
        public string lName { set; get; }
        public string sName { set; get; }
        public string email { set; get; }
        public string password { set; get; }
        public int groupId { set; get; }
        public ERoles role { set; get; }
        public DateTime registrationDate { set; get; }
        public DateTime lastOnlineDate { set; get; }
        
        public User
            (
                int id,
                string fName,
                string lName,
                string sName,
                string email,
                string password,
                int groupId,
                ERoles role,
                DateTime registrationDate,
                DateTime lastOnlineDate
            )
        {
            this.id = id;
            this.fName = fName;
            this.lName = lName;
            this.sName = sName;
            this.email = email;
            this.password = password;
            this.groupId = groupId;
            this.role = role;
            this.registrationDate = registrationDate;
            this.lastOnlineDate = lastOnlineDate;
        }
        public User
            (
                int id,
                string fName,
                string lName,
                string sName,
                string email,
                string password,
                int groupId,
                ERoles role,
                DateTime registrationDate
            )
        {
            this.id = id;
            this.fName = fName;
            this.lName = lName;
            this.sName = sName;
            this.email = email;
            this.password = password;
            this.groupId = groupId;
            this.role = role;
            this.registrationDate = registrationDate;
        }
        public override string ToString()
        {
            return "id: " + this.id +
                "\nfirst name: " + this.fName +
                "\nlast name: " + this.lName +
                "\nsecond name: " + this.sName +
                "\nemail: " + this.email +
                "\npassword: " + this.password +
                "\ngroupId: " + this.groupId +
                "\nrole: " + this.role.ToString() +
                "\nregistration date: " + this.registrationDate +
                "\nlast online date: " + this.lastOnlineDate;
        }
    }
}