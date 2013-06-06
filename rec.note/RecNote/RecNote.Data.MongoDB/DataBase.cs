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
        
        protected MongoCollection<T> GetCollection()
        {
            return ((RecNote.Data.MongoDB.GenericDataBase)GenericDataBase).MongoDataBase.GetCollection<T>(typeof(T).FullName);
        }

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

        public void PartialUpdate(string id, string path, object obj)
        {
            var query = Query.EQ("_id", BsonValue.Create(id));
            //var update = Update.Set(path, obj);
            this.GetCollection().Update(query, Update.Set(path, BsonDocument.Create(obj)));
        }
    }
}
