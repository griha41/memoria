using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data
{
    /// <summary>
    /// Busqueda general de datos
    /// </summary>
    /// <typeparam name="T">Tipo de datos</typeparam>
    public interface IDataBase<T>
    {
        /// <summary>
        /// Busca un elemento vía su identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        T FindByID(string id);
        /// <summary>
        /// Borra un elemento vía su identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        bool Remove(string id);
        /// <summary>
        /// Guarda un elemento
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        T Save(T entry);
    }
}
