using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace RecNote.Data.MongoDB
{
    public class DataBase<T> : IDataBase<T> where T : Entities.Base
    {
        protected IGenericDataBase GenericDataBase { get; set; }

        public T FindByID(string id)
        {
            return this.GenericDataBase.FindByID<T>(id);
        }

        public T Save(T entry)
        {
            return this.GenericDataBase.Save<T>(entry);
        }

        public bool Remove(string id)
        {
            return this.GenericDataBase.Remove<T>(id);
        }

    }
}
