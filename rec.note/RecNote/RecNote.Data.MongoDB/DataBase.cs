using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using RecNote.Data;

namespace RecNote.Data.MongoDB
{
    public class DataBase<T> : IDataBase<T> where T : Entities.Base
    {
        protected GenericDataBase GenericDataBase { get; set; }
        
        protected MongoCollection<T> GetCollection()
        {
            return this.GenericDataBase.MongoDataBase.GetCollection<T>(typeof(T).FullName);
        }

        public virtual T FindByID(string id)
        {
            return this.GenericDataBase.FindByID<T>(id);
        }

        public virtual T Save(T entry)
        {
            return this.GenericDataBase.Save<T>(entry);
        }

        public virtual bool Remove(string id)
        {
            return this.GenericDataBase.Remove<T>(id);
        }

        public virtual void PartialUpdate(string id, string path, object obj)
        {
            var query = Query.EQ("_id", BsonValue.Create(id));
            //var update = Update.Set(path, obj);
            this.GetCollection().Update(query, Update.Set(path, BsonDocument.Create(obj)));
        }
    }
}
