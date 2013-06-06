using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class ProjectDefinition
    {
        public ProjectItem Introducction { get; set; }
        public ProjectItem[] Actors { get; set; }
        public ProjectItem CurrentSystem { get; set; }
        public ProjectItem[] Objetives { get; set; }
    }
}
