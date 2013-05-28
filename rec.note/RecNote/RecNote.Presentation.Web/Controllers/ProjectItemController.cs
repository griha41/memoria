using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Projects;
using RecNote.Domain.Core.Projects;
using Model = RecNote.Presentation.Web.Models.ProjectItem;

namespace RecNote.Presentation.Web.Controllers
{
    public class ProjectItemController : BaseController
    {
        //
        // GET: /ProjectItem/
        IProjectProvider ProjectProvider { get; set; }
        

        public ActionResult Preview(Project project, ProjectItem item, ProjectItem parent)
        {
            
            return View(new Model.View
            {
                Project = project,
                Item = item,
                Parent = parent
            });
        }

        public ActionResult View(string projectID, string itemName, string parentName)
        {
            var project = this.ProjectProvider.FindByID(projectID);

            var parents = from e in project.Information
                          from p in e.Childs
                          select p;

            var parent = (string.IsNullOrEmpty(parentName) ? null : parents.FirstOrDefault(p => p.Name == parentName));
            var item = ((parent == null) ? parents : parent.Childs).FirstOrDefault(p => p.Name == itemName);

            return View(new Model.View
            {
                Project = project,
                Item = item,
                Parent = parent
            });
        }

        [ValidateInput(false)]
        public ActionResult AllowEdit(string projectID, string itemName, string parentName)
        {
            var project = this.ProjectProvider.FindByID(projectID);

            var parents = from e in project.Information
                          from p in e.Childs
                          select p;

            var parent = (string.IsNullOrEmpty(parentName) ? null : parents.FirstOrDefault(p => p.Name == parentName));
            var item = ((parent == null) ? parents : parent.Childs).FirstOrDefault(p => p.Name == itemName);

            if (item.State != ProjectItemStateType.OnEdit)
                return Json(item.Data, JsonRequestBehavior.AllowGet);

            throw new Exception("error.projectItemOnEdit");
        }

    }
}
