using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data
{
    public interface IGenericDataBase
    {
        T FindByID<T>(string id) where T : RecNote.Entities.Base;
        T[] ListByIDs<T>(string[] ids) where T : RecNote.Entities.Base;
        bool Remove<T>(string id) where T : RecNote.Entities.Base;
        T Save<T>(T entry)  where T : RecNote.Entities.Base;
    }
}
