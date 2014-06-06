using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Session
{
    /// <summary>
    /// Información de una sesión de usuario
    /// </summary>
    public class Session : Base
    {
        /// <summary>
        /// Usuario
        /// </summary>
        public Entities.Users.User User { get; set; }
        /// <summary>
        /// Cuando inició
        /// </summary>
        public DateTime Time { get; set; }
    }
}
