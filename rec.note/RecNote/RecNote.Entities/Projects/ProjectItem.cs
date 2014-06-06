using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Unidad de información mínima del proyecto
    /// </summary>
    public class ProjectItem
    {
        /// <summary>
        /// Información o definición realizada
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// Indica si es publico para ser visualizado
        /// </summary>
        public bool IsPublic { get; set; }
        /// <summary>
        /// Nombre del concepto, también es su identificador dentro de una lista
        /// </summary>
        [BsonId]
        public string Name { get; set; }
        /// <summary>
        /// Último editor
        /// </summary>
        public string EditorID { get; set; }
        /// <summary>
        /// Estado de la información. En edición o Guardado.
        /// </summary>
        public ProjectItemStateType State { get; set; }
        /// <summary>
        /// Comentarios del concepto.
        /// </summary>
        public ProjectItemComment[] Comments { get; set; }

        /// <summary>
        /// Constructor que permite nombrar al item en su creación
        /// </summary>
        /// <param name="name">Nombre del concepto</param>
        public ProjectItem(string name) : this()
        {
            this.Name = name;
        }
        /// <summary>
        /// Constructor básico que inicializa los comentarios
        /// </summary>
        public ProjectItem() {
            this.Comments = new ProjectItemComment[] { };
        }
        
    }
}
