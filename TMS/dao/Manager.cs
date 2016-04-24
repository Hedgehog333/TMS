using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.dao
{
    public  class Manager<T> 
        where T : class, new()
    {
        private static Manager<T> _instance = null;
        private static readonly object _lock = new object();
        public T Dao = new T();
        protected Manager()
        {}
        public static Manager<T> Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Manager<T>();
                    }
                }
                return _instance;
            }
        }
    }
}