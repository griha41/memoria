using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecNote.Presentation.Web.Models.Audio
{
    public class List
    {
        public string ProjectID { get; set; }
        public Entities.Audio.Audio[] Audio { get; set; }
    }
}