using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.Project
{
    public class Actor : Entities.Projects.Actor
    {
        public string ProjectID { get; set; }
    }
}