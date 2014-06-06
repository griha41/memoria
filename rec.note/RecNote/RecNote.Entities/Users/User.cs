using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Users
{
     /// <summary>
     /// Información de usuario
     /// </summary>
    [BsonKnownTypes(typeof(Projects.Actor))]
    [BsonDiscriminator(RootClass=true)]
    public class User : Base
    {
        /// <summary>
        /// Correo electronico
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Contraseña
        /// </summary>
        public string Password { get; set; }
    }
}
