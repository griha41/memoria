using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecNote.Entities.Projects;

namespace RecNote.Presentation.Web.Models.Project
{
    public class Actor
    {
        public Entities.Projects.Actor User { get; set; }
        public string ProjectID { get; set; }

        public Actor()
        {
            this.User = new Entities.Projects.Actor();
        }
    }
}