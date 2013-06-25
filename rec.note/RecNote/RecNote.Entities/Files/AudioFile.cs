using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files
{
    public class AudioFile : File
    {
        public string AlterPath { get; set; }
        public bool[][] Finger { get; set; }
    }
}
