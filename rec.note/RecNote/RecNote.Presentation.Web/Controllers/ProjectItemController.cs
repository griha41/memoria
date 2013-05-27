using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Projects;
using Model = RecNote.Presentation.Web.Models.ProjectItem;

namespace RecNote.Presentation.Web.Controllers
{
    public class ProjectItemController : BaseController
    {
        //
        // GET: /ProjectItem/

        public ActionResult View(Project project, ProjectItem item, ProjectItem parent)
        {
            
            return View(new Model.View
            {
                Project = project,
                Item = item,
                Parent = parent
            });
        }

    }
}
