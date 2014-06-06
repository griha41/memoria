using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data.Files
{
    /// <summary>
    /// Interface de manejo de datos de archivos, hereda de manejo de archivo basados en archivos
    /// </summary>
    public interface IFileData : IDataBase<Entities.Files.File>
    {
        /// <summary>
        /// Lista los archivos por su tipo
        /// </summary>
        /// <param name="type">tipo buscado</param>
        /// <returns></returns>
        Entities.Files.File[] FindByType(string type);
    }
}
