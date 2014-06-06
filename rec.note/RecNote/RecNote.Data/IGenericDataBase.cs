using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data
{
    /// <summary>
    /// Uso generico de guardado de datos
    /// </summary>
    public interface IGenericDataBase
    {
        /// <summary>
        /// Busqueda de T vía su identificador
        /// </summary>
        /// <typeparam name="T">Tipo de elemento</typeparam>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        T FindByID<T>(string id) where T : RecNote.Entities.Base;
        /// <summary>
        /// Busqueda de un elemento derivado de otro
        /// </summary>
        /// <typeparam name="T">Elemento padre</typeparam>
        /// <typeparam name="R">Elemento derivado</typeparam>
        /// <param name="id">Identificador de elemento derivado</param>
        /// <returns></returns>
        R FindByID<T,R>(string id) where T : RecNote.Entities.Base where R : T;
        /// <summary>
        /// Listado generico
        /// </summary>
        /// <typeparam name="T">Tipo de elemtno</typeparam>
        /// <param name="ids">Identificadores</param>
        /// <returns></returns>
        T[] ListByIDs<T>(string[] ids) where T : RecNote.Entities.Base;
        /// <summary>
        /// Borrado generico
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <param name="id">Identificador a borrar</param>
        /// <returns></returns>
        bool Remove<T>(string id) where T : RecNote.Entities.Base;
        /// <summary>
        /// Guardado generico
        /// </summary>
        /// <typeparam name="T">Tipo de dato</typeparam>
        /// <param name="entry">Elemento guardado</param>
        /// <returns></returns>
        T Save<T>(T entry)  where T : RecNote.Entities.Base;
    }
}
