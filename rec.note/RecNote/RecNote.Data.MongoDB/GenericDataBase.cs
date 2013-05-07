﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;


namespace RecNote.Data.MongoDB
{
    public class GenericDataBase : IGenericDataBase
    {
        #region MongoDatabase
        private MongoDatabase mongoDataBase { get; set; }
        public MongoDatabase MongoDataBase { get {
            if (this.mongoDataBase == null)
            {
                var client = new MongoClient(this.ConnectionString);
                this.mongoDataBase = client.GetServer().GetDatabase(this.Database);
            }
            return this.mongoDataBase;
        } set { this.mongoDataBase = value; } }
        #endregion
        public string ConnectionString { get; set; }
        public string Database { get; set; }

        
        protected MongoCollection<Container<T>> GetCollection<T>() where T : RecNote.Entities.Base
        {
            return this.MongoDataBase.GetCollection<Container<T>>(typeof(T).FullName);
        }

        public T FindByID<T>(string id) where T : RecNote.Entities.Base
        {
            return this.GetCollection<T>().Find(query: Query<Container<T>>.EQ(e => e.Id, new ObjectId( id ))).FirstOrDefault();
        }

        public bool Remove<T>(string id) where T : RecNote.Entities.Base
        {
            return this.GetCollection<T>().Remove(query: Query<Container<T>>.EQ(e => e.Id, new ObjectId( id ))).Ok;
        }

        public T Save<T>(T entry) where T : RecNote.Entities.Base
        {
            var con = new Container<T>(entry);
            var e = this.GetCollection<T>().Save(con);
            entry.ID = con.Id.ToString();
            return entry;
        }
    }
}