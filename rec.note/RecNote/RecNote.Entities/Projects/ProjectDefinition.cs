using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Región de definiciones del proyecto
    /// </summary>
    public class ProjectDefinition
    {
        /// <summary>
        /// Introducción
        /// </summary>
        public ProjectItem Introducction { get; set; }
        /// <summary>
        /// Actores
        /// </summary>
        public ProjectItem[] Actors { get; set; }
        /// <summary>
        /// Información del sistema actual
        /// </summary>
        public ProjectItem CurrentSystem { get; set; }
        /// <summary>
        /// Objetivos del proyecto
        /// </summary>
        public ProjectItem[] Objetives { get; set; }
    }
}
