using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data;

namespace RecNote.Domain.Core.Base
{
    public class ProviderBase<T> : IProviderBase<T> where T : Entities.Base
    {
        private IGenericDataBase GenericDataBase { get; set; }

        public T FindByID(string id) 
        {
            return this.GenericDataBase.FindByID<T>(id);
        }

        public bool Remove(string id)
        {
            return this.GenericDataBase.Remove<T>(id);
        }

        public T Save(T entry)
        {
            return this.GenericDataBase.Save<T>(entry);
        }
    }
}
