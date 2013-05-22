using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.ProjectItem
{
    public class View
    {
        public RecNote.Entities.Projects.Project Project { get; set; }
        public RecNote.Entities.Projects.InformationNode Item { get; set; }
        public string Name { get; set; }
    }
}