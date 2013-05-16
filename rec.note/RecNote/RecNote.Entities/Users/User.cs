using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Users
{
     
    [BsonKnownTypes(typeof(Projects.Actor))]
    [BsonDiscriminator(RootClass=true)]
    public class User : Base
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
