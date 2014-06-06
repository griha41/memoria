using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files.Compared
{
    /// <summary>
    /// Clase para comparación de archivos
    /// </summary>
    public class AudioComparedResult
    {
        /// <summary>
        /// Archivo 1 a comparar
        /// </summary>
        public Entities.Files.File File1 { get; set; }
        /// <summary>
        /// Archivo 2 a coparar
        /// </summary>
        public Entities.Files.File File2 { get; set; }
        /// <summary>
        /// Resultados de comparaciones
        /// </summary>
        public AudioComparedResultItem[] items { get; set; }
    }
}
