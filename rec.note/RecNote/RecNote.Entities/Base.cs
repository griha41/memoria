using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace RecNote.Entities
{
    [Serializable]
    public class Base
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }
    }
}
