using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data
{
    public interface IDataBase<T>
    {
        T FindByID(string id);
        bool Remove(string id);
        T Save(T entry);
    }
}
