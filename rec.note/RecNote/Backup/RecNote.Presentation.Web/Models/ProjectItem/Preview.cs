using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.ProjectItem
{
    public class Preview
    {
        public Entities.Projects.Project Project { get; set; }
        public Entities.Projects.ProjectItem[] Items { get; set; }
        public ProjectItemType Type { get; set; }
    }
}