using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Audio;
namespace RecNote.Presentation.Web.Models.Audio
{
    public class New
    {
        public string ProjectID { get; set; }
        public Entities.Audio.Audio Audio { get; set; }
    }
}