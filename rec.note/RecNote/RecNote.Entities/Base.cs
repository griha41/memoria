using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace RecNote.Entities
{
    /// <summary>
    /// Estructura base de todo elemento
    /// </summary>
    [Serializable]
    public class Base
    {
        /// <summary>
        /// Identificador interno que hace único a este elemento del resto
        /// </summary>
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string ID { get; set; }
    }
}
