using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Audio
{
    /// <summary>
    /// Clade de audio, guarda la información del archivo y nombre.
    /// </summary>
    public class Audio : Base
    {
        /// <summary>
        /// Identificador del proyecto
        /// </summary>
        public string ProjectID { get; set; }
        /// <summary>
        /// Identificador del archivo asociado
        /// </summary>
        public string FileID { get; set; }
        /// <summary>
        /// Nombre del audio
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Fecha de agregación
        /// </summary>
        public DateTime Date { get; set; }
    }
}
