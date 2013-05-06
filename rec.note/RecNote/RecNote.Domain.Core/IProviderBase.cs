using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RecNote.Domain.Core
{
    public interface IProviderBase<T> where T : Entities.Base
    {
        T FindByID(string id);
        bool Remove(string id);
        T Save(T entry);
    }
}
