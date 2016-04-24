using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.dao
{
    interface IDAO<T>
        where T : class
    {
        T get(int id);
        List<T> getAll();
        void add(T item);
        void update(T item);
        void delete(T item);
    }
}
