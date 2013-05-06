using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RecNote.Domain.Core
{
    public interface IProviderBase
    {
        T FindByID<T>(string id) where T : Entities.Base;
        bool Remove<T>(string id) where T : Entities.Base;
        T Save<T>(T entry) where T : Entities.Base;
    }
}
