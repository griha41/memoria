using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Proyecto: Guarda la información relevante de un proyecto de elicitación de requisitos.
    /// </summary>
    public class Project : Base
    {
        /// <summary>
        /// Nombre del proyecto
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Indica si el proyecto es temporal
        /// </summary>
        public bool IsTemporal { get; set; }

        /// <summary>
        /// Usuario dueño del proyecto
        /// </summary>
        public Users.User Owner { get; set; }

        /// <summary>
        /// Actores integrantes del proyecto
        /// </summary>
        public Actor[] Actors { get; set; }

        /// <summary>
        /// Estado del proyecto
        /// </summary>
        public ProjectStateType State { get; set; }

        #region Information 
        /// <summary>
        /// Región de definiciones del proyecto
        /// </summary>
        public ProjectDefinition Definition { get; set; }
        /// <summary>
        /// Región de requisitos del proyecto
        /// </summary>
        public ProjectRequirement Requirements {get;set;}
        #endregion

    }
}
