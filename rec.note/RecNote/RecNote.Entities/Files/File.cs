using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Runtime.Serialization;

namespace RecNote.Entities.Files
{
    [BsonKnownTypes(typeof(Files.AudioFile))]
    [BsonDiscriminator(RootClass = true)]
    [Serializable]
    public class File : Entities.Base
    {
        
        public string FullPath { get; set; }
        
        public string Name { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
    }
}
