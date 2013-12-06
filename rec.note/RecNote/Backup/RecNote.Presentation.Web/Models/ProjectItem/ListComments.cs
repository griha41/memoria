using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Projects;
using RecNote.Entities.Users;

namespace RecNote.Presentation.Web.Models.ProjectItem
{
    public class ListComments
    {
        public Actor[] Actors { get; set; }
        public ProjectItemComment[] Comments { get; set; }
        public string ProjectID { get; set; }
        public string ItemName { get; set; }
        public ProjectItemType Type { get; set; }
    }
}