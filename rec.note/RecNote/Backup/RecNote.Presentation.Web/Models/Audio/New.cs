using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Audios;
namespace RecNote.Presentation.Web.Models.Audio
{
    public class New
    {
        public string ProjectID { get; set; }
        public Entities.Audios.Audio Audio { get; set; }
    }
}