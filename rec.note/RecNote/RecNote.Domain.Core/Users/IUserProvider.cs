using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Users;

namespace RecNote.Domain.Core.Users
{
    /// <summary>
    /// Administrador de usuario
    /// </summary>
    public interface IUserProvider : IProviderBase<User>
    {
        /// <summary>
        /// Inicio de sesión de usuario
        /// </summary>
        /// <param name="email">Correo electronico</param>
        /// <param name="password">Contraseña</param>
        /// <returns></returns>
        bool Login(string email, string password);
        /// <summary>
        /// Busqueda de usuario via correo electronico
        /// </summary>
        /// <param name="email">Correo electronico</param>
        /// <returns></returns>
        User FindByEmail(string email);
        /// <summary>
        /// Crear usuario
        /// </summary>
        /// <param name="user">Usuario a crear</param>
        /// <returns></returns>
        User New(User user);
        /// <summary>
        /// Reset de contraseña para usuario
        /// </summary>
        /// <param name="user">Usuario a resetear</param>
        void NewPassword(User user);
    }
}
