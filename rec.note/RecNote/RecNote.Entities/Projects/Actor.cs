using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Projects
{
    public class Actor : Users.User
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public RoleType Role { get; set; }
    }
}
