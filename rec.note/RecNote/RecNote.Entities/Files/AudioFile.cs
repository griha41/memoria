using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files
{
    [Serializable]
    public class AudioFile : File
    {
        public string AlterPath { get; set; }
        public bool[][] Finger { get; set; }

        public string AudioID { get; set; }
    }
}
