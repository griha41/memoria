using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Session
{
    /// <summary>
    /// Administra las sesiones de usuario
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// Nueva sesión de usuario
        /// </summary>
        /// <param name="user">Usuario logeado</param>
        /// <returns></returns>
        Entities.Session.Session New(Entities.Users.User user);
        /// <summary>
        /// Encontrar una sesión por su identificador
        /// </summary>
        /// <param name="id">Identificador de la sesión</param>
        /// <returns></returns>
        Entities.Session.Session FindByID(string id);
    }
}
