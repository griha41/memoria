using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Indica el estado el proyecto
    /// </summary>
    public enum ProjectItemStateType
    {
        /// <summary>
        /// Está siendo editado
        /// </summary>
        OnEdit = 1,
        /// <summary>
        /// No está siendo editado
        /// </summary>
        Save = 2
    }
}
