using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data;

namespace RecNote.Domain.Core.Base
{
    public class ProviderBase : IProviderBase
    {
        protected IGenericDataBase GenericDataBase { get; set; }

        public T FindByID<T>(string id) where T : Entities.Base
        {
            return this.GenericDataBase.FindByID<T>(id);
        }

        public bool Remove<T>(string id) where T : Entities.Base
        {
            return this.GenericDataBase.Remove<T>(id);
        }

        public T Save<T>(T entry) where T : Entities.Base
        {
            return this.GenericDataBase.Save<T>(entry);
        }
    }
}
