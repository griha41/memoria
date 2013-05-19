using System;
using System.Collections.Generic;
using System.Linq;

using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.Project
{
    public class List
    {
        public IList<RestrictedProject> RestrictedProjects { get; set; }
    }
}
