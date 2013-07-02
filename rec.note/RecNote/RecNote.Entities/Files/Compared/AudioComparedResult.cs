using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Files.Compared
{
    public class AudioComparedResult
    {
        public Entities.Files.File File1 { get; set; }
        public Entities.Files.File File2 { get; set; }
        public AudioComparedResultItem[] items { get; set; }
    }
}
