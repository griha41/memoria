using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Files
{
    public class AudioComparedResultItem
    {
        public double MillisecondFile1 { get; set; }
        public double MillisecondFile2 { get; set; }
        public double Similitary { get; set; }
    }
}
