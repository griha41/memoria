using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.Project
{
    public class Project
    {
        public Entities.Projects.Project CurrentProject { get; set; }
    }
}