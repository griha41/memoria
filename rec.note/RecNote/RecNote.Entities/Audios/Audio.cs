using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Audios
{
    public class Audio : Base
    {
        public string[] Projects { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
