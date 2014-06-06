using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    /// <summary>
    /// Comentario de un concepto
    /// </summary>
    public class ProjectItemComment
    {
        /// <summary>
        /// Usuario que lo agregó
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Texto del mensaje
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Cuando fue creado
        /// </summary>
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public DateTime Time { get; set; }
    }
}
