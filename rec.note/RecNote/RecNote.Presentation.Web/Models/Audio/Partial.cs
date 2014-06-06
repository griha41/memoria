using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecNote.Presentation.Web.Models.Audio
{
    public class Partial
    {
        public int Init { get; set; }
        public int End { get; set; }
        public Entities.Audio.Audio Audio { get; set; }
        public double Similitary { get; set; }
    }
}