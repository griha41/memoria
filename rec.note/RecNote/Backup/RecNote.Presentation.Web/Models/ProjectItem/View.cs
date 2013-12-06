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
        public RecNote.Entities.Projects.ProjectItem Item { get; set; }
        public ProjectItemType Type { get; set; }
        public Entities.Projects.RoleType Role { get; set; }
    }
}