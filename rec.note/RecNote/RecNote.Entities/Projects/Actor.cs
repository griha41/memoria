using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Un actor deriva de un usuario del sistema al cual se le asigna un rol.
    /// </summary>
    public class Actor : Users.User
    {
        /// <summary>
        /// Rol que le es asignado al actor, depende del proyecto que tenga asociado el actor.
        /// </summary>
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public RoleType Role { get; set; }
    }
}
