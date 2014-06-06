using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
using System.Runtime.Serialization;

namespace RecNote.Entities.Files
{
    /// <summary>
    /// Clase de archivo, lista la información de metada básica del archivo guardado
    /// </summary>
    [BsonKnownTypes(typeof(Files.AudioFile))]
    [BsonDiscriminator(RootClass = true)]
    [Serializable]
    public class File : Entities.Base
    {
        /// <summary>
        /// Ruta completa donde se encuentra físicamente el archivo
        /// </summary>
        public string FullPath { get; set; }
        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// URL para su obtención
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Tamaño en kilobytes del archivo
        /// </summary>
        public long Size { get; set; }
        /// <summary>
        /// Tipo del contenido
        /// </summary>
        public string ContentType { get; set; }
    }
}
