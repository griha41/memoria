using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RecNote.Presentation.Web.Models.ProjectItem
{
    public class ViewComment : View
    {
        public RecNote.Entities.Projects.ProjectItemComment Comment { get; set; }
        public RecNote.Entities.Projects.Actor[] Actors { get; set; }
        public RecNote.Entities.Projects.Actor Actor { get; set; }
    }
}