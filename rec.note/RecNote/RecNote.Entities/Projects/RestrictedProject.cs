using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Información de permisos sobre el proyecto
    /// </summary>
    public class RestrictedProject
    {
        /// <summary>
        /// Rol
        /// </summary>
        public RoleType Role { get; set; }
        /// <summary>
        /// Indica si es el dueño
        /// </summary>
        public bool IsOwner { get; set; }
        /// <summary>
        /// Información del proyecto
        /// </summary>
        public Project Project { get; set; }
        /// <summary>
        /// Información del usuario
        /// </summary>
        public Users.User UserAllow { get; set; }
    }
}
