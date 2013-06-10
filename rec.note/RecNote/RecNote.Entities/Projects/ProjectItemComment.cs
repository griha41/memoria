using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class ProjectItemComment
    {
        public string UserID { get; set; }
        public string Message { get; set; }
        [MongoDB.Bson.Serialization.Attributes.BsonId]
        public DateTime Time { get; set; }
    }
}
