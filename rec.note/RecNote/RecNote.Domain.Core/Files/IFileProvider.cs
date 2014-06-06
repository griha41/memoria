using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Files;

namespace RecNote.Domain.Core.Files
{
    /// <summary>
    /// Manejador de Archivos
    /// </summary>
    public interface IFileProvider : IProviderBase<Entities.Files.File>
    {
        /// <summary>
        /// Sube un archivo al sistema
        /// </summary>
        /// <param name="name">Nombre del archivo</param>
        /// <param name="contentType">Tipo de contenido</param>
        /// <param name="file">Bytes del archivo</param>
        /// <returns></returns>
        File Upload(string name, string contentType, byte[] file);
        /// <summary>
        /// Obtiene los bytes de un archivo guardao
        /// </summary>
        /// <param name="fileID">Identificador del archivo</param>
        /// <returns></returns>
        Byte[] GetFile(string fileID);
        /// <summary>
        /// Lista archivos según su tipo
        /// </summary>
        /// <param name="type">Tipo de archivos buscados</param>
        /// <returns></returns>
        File[] FindByType(string type);
        /// <summary>
        /// Busca un archivo derivado según su identificador
        /// </summary>
        /// <typeparam name="TF">Tipo de archivo derivado, por ejemplo AudioFile</typeparam>
        /// <param name="fileID">Identificador del archivo</param>
        /// <returns></returns>
        TF FindByID<TF>(string fileID) where TF : Entities.Files.File;
        /// <summary>
        /// Obtiene un fragmento del archivo
        /// </summary>
        /// <param name="fileID">Identificador</param>
        /// <param name="start">Comienzo del archivo</param>
        /// <param name="end">Byte de fin</param>
        /// <returns></returns>
        Byte[] GetFile(string fileID, int start, int end);
    }
}
