using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace RecNote.Domain.Core
{
    /// <summary>
    /// Interface básica de un administrador de tipo
    /// </summary>
    /// <typeparam name="T">Tipo administrado</typeparam>
    public interface IProviderBase<T> where T : Entities.Base
    {
        /// <summary>
        /// Busca el tipo a través de su identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        T FindByID(string id);
        /// <summary>
        /// Busca los elementos a través de sus identificadores
        /// </summary>
        /// <param name="ids">Identificadores</param>
        /// <returns></returns>
        T[] ListByIDs(string[] ids);
        /// <summary>
        /// Borra los elementos a través de sus identificador
        /// </summary>
        /// <param name="id">Identificador</param>
        /// <returns></returns>
        bool Remove(string id);
        /// <summary>
        /// Guarda un elemento del tipo asociado
        /// </summary>
        /// <param name="entry">Elemento a guardar</param>
        /// <returns></returns>
        T Save(T entry);
    }
}
