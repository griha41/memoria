using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files
{
    /// <summary>
    /// Archivo de audio, deriva de un archivo
    /// </summary>
    [Serializable]
    public class AudioFile : File
    {
        /// <summary>
        /// Indica la ruta del audio procesado (Luego de bajar su calidad y transformación a mp3)
        /// </summary>
        public string AlterPath { get; set; }
        /// <summary>
        /// Firma del archivo
        /// </summary>
        public bool[][] Finger { get; set; }
        /// <summary>
        /// Identificador del audio asociado
        /// </summary>
        public string AudioID { get; set; }
    }
}
