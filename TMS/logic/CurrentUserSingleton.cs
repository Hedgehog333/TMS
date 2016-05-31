using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.data;
namespace TMS.logic
{
    public class CurrentUserSingleton
    {
        private User user;
        public User User {
            get
            {
                return this.user;
            }
        }
        private CurrentUserSingleton()
        {
        }
        public static CurrentUserSingleton Instance
        {
            get
            {
                if (InstanceHolper._instance.user == null)
                    InstanceHolper._instance.user = LoginMethod.getUser();
                return InstanceHolper._instance;
            }
        }
        protected class InstanceHolper
        {
            static InstanceHolper() { }
            internal static CurrentUserSingleton _instance = new CurrentUserSingleton();
        }
        /// <summary>
        /// If needed logout.
        /// </summary>
        public static void Clear()
        {
            InstanceHolper._instance.user = null;
        }
    }
}