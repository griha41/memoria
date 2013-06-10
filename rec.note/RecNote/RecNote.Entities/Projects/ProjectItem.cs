using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace RecNote.Entities.Projects
{
    public class ProjectItem
    {
        public string Data { get; set; }
        public bool IsPublic { get; set; }

        [BsonId]
        public string Name { get; set; }

        public string EditorID { get; set; }
        
        public ProjectItemStateType State { get; set; }

        public ProjectItemComment[] Comments { get; set; }


        public ProjectItem(string name) : this()
        {
            this.Name = name;
        }
        public ProjectItem() {
            this.Comments = new ProjectItemComment[] { };
        }
        
    }
}
