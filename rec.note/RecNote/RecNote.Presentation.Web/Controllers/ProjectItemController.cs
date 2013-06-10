using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Projects;
using RecNote.Domain.Core.Projects;
using RecNote.Domain.Core.Users;
using Model = RecNote.Presentation.Web.Models.ProjectItem;

namespace RecNote.Presentation.Web.Controllers
{
    public class ProjectItemController : BaseController
    {
        //
        // GET: /ProjectItem/
        IProjectProvider ProjectProvider { get; set; }
        IUserProvider UserProvider { get; set; }


        public ActionResult Preview(Project project, ProjectItem[] items, ProjectItemType type)
        {
            if(type == ProjectItemType.Introduction || type == ProjectItemType.CurrentSystem)
                return View(new Model.View
                {
                    Project = project,
                    Item = items.FirstOrDefault(),
                    Type = type
                });
            else
                return View("PreviewResume", new Model.Preview
                {
                    Project = project,
                    Items = items,
                    Type = type
                });
        }

        public ActionResult View(string projectID, ProjectItemType type, string name)
        {
            var project = this.ProjectProvider.FindByID(projectID);

            if (type == ProjectItemType.Introduction || type == ProjectItemType.CurrentSystem || !string.IsNullOrEmpty(name))
            {
                var item = this.ProjectProvider.GetItem(projectID, type, name);
                return View(new Model.View
                {
                    Project = project,
                    Item = item,
                    Type = type
                });
            }
            else
            {
                ProjectItem[] items = null;
                switch(type)
                {
                    case ProjectItemType.Actors: items = project.Definition.Actors; break;
                    case ProjectItemType.Objetives: items = project.Definition.Objetives; break;
                    case ProjectItemType.ReqFunctionals: items = project.Requirements.Functionals; break;
                    case ProjectItemType.ReqInformations: items = project.Requirements.Informations; break;
                    case ProjectItemType.ReqNotFunctionals: items = project.Requirements.NotFunctionals; break;
                }
                return View("ViewArray", new Model.Preview
                {
                    Project = project,
                    Items = items,
                    Type = type
                });
            }

        }

        [ValidateInput(false)]
        public ActionResult AllowEdit(string projectID, ProjectItemType type, string name)
        {
            var item = this.ProjectProvider.BlockItem(projectID, type, name, MvcApplication.CurrentUser);
            return Json(item, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveItem(string projectID, ProjectItemType type,string name, ProjectItem item)
        {
            var oldItem = this.ProjectProvider.GetItem(projectID, type, name);
            if (string.IsNullOrEmpty(item.Name))
                oldItem.Name = item.Name;
            oldItem.Data = item.Data;
            return Json(this.ProjectProvider.SaveItem(projectID, type, oldItem), JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult Publish(string projectID, ProjectItemType type, string name, bool publish)
        {
            return Json(this.ProjectProvider.PublishItem(projectID, type, name, publish), JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListComments(string projectID, ProjectItemType type, string name)
        {
            var item = this.ProjectProvider.GetItem(projectID, type, name);
            var project = this.ProjectProvider.FindByID(projectID);
            return View(
                new Model.ListComments
                {
                    Comments = item.Comments,
                    ProjectID = project.ID,
                    Type = type,
                    ItemName = name,
                    Actors = project.Actors.Concat(
                        this.UserProvider.ListByIDs(
                            item.Comments.Where(c => !project.Actors.Any(a => a.ID == c.UserID))
                            .Select(c => c.UserID).ToArray()
                            ).Select(u => new Actor
                            {
                                ID = u.ID,
                                Email = u.Email,
                                Name = u.Email,
                                Role = (u.ID == project.Owner.ID) ? RoleType.Developer : RoleType.Stakeholder
                            })
                            ).ToArray()
                }
                );
        }
        [ValidateInput(false)]
        public ActionResult NewComment(string projectID, ProjectItemType type, string name)
        {
            var model = new Model.View {
                Item = this.ProjectProvider.GetItem(projectID, type, name),
                Type = type,
                Project = this.ProjectProvider.FindByID(projectID)
            };
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult ViewComment(string projectID, ProjectItemType type, string name, long timeTicks)
        {
            var item = this.ProjectProvider.GetItem(projectID, type, name);
            var project = this.ProjectProvider.FindByID(projectID);
            var actors = project.Actors.Concat(
                        this.UserProvider.ListByIDs(
                            item.Comments.Where(c => !project.Actors.Any(a => a.ID == c.UserID))
                            .Select(c => c.UserID).ToArray()
                            ).Select(u => new Actor
                            {
                                ID = u.ID,
                                Email = u.Email,
                                Name = u.Email,
                                Role = (u.ID == project.Owner.ID) ? RoleType.Developer : RoleType.Stakeholder
                            })
                            ).ToArray();
            var comment = item.Comments.Where(c => c.Time.Ticks == timeTicks).FirstOrDefault();
            var model = new Model.ViewComment
            {
                Project = project,
                Item = item,
                Comment = comment,
                Type = type,
                Actors = actors,
                Actor = actors.FirstOrDefault(a => a.ID == comment.UserID)
            };
            return View(model);
        }


        public ActionResult AddComment(string projectID, ProjectItemType type, string name, string message)
        {
            this.ProjectProvider.AddComment(projectID, type, name, message, MvcApplication.CurrentUser);
            return null;
        }
    }
}
