using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class ProjectRequirement
    {
        public ProjectItem[] Informations { get; set; }
        public ProjectItem[] Functionals { get; set; }
        public ProjectItem[] NotFunctionals { get; set; }
    }
}
