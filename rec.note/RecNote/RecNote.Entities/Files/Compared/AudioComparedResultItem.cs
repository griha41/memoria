using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files.Compared
{
    public class AudioComparedResultItem
    {
        /// <summary>
        /// Milisegundo del archivo 1
        /// </summary>
        public double MillisecondFile1 { get; set; }
        /// <summary>
        /// Milisegundo del archivo 2
        /// </summary>
        public double MillisecondFile2 { get; set; }
        /// <summary>
        /// Similaridad entre milisegundos
        /// </summary>
        public double Similitary { get; set; }
    }
}
