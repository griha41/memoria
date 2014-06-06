using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Indica el tipo de concepto permitido
    /// </summary>
    public enum ProjectItemType
    {
        /// <summary>
        /// Introducción
        /// </summary>
        Introduction = 1,
        /// <summary>
        /// Actores
        /// </summary>
        Actors = 2,
        /// <summary>
        /// Información del actual sistema
        /// </summary>
        CurrentSystem = 3,
        /// <summary>
        /// Objetivos
        /// </summary>
        Objetives = 4,
        /// <summary>
        /// Requerimientos de información
        /// </summary>
        ReqInformations = 5,
        /// <summary>
        /// Requerimientos funcionales
        /// </summary>
        ReqFunctionals = 6,
        /// <summary>
        /// Requerimientos no funcionales
        /// </summary>
        ReqNotFunctionals = 7
    }
}
