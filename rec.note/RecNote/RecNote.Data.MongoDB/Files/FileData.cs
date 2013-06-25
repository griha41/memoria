using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB;

namespace RecNote.Data.MongoDB.Files
{
    public class FileData : DataBase<Entities.Files.File>, RecNote.Data.Files.IFileData
    {

        public Entities.Files.File[] FindByType(string type)
        {
            return (
                from e in this.GetCollection().AsQueryable()
                where e.ContentType.ToLower().Contains(type.ToLower())
                select e
             ).ToArray();
        }
    }
}
