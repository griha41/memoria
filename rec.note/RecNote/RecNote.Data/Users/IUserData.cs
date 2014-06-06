using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Users;

namespace RecNote.Data.Users
{
    /// <summary>
    /// Información de la información de un usuario
    /// </summary>
    public interface IUserData : IDataBase<User>
    {
        /// <summary>
        /// Busqueda por correo electronico
        /// </summary>
        /// <param name="email">Correo electronico</param>
        /// <returns></returns>
        User FindByEmail(string email);
    }
}
