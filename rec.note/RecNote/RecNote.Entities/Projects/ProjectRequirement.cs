using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Información de requerimientos del proyecto
    /// </summary>
    public class ProjectRequirement
    {
        /// <summary>
        /// Requerimientos de información
        /// </summary>
        public ProjectItem[] Informations { get; set; }
        /// <summary>
        /// Requerimientos funcionales
        /// </summary>
        public ProjectItem[] Functionals { get; set; }
        /// <summary>
        /// Requerimientos no funcionales
        /// </summary>
        public ProjectItem[] NotFunctionals { get; set; }
    }
}
