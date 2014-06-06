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
    /// <summary>
    /// Controlador de conceptos del proyecto
    /// </summary>
    public class ProjectItemController : BaseController
    {
        //
        // GET: /ProjectItem/
        IProjectProvider ProjectProvider { get; set; }
        IUserProvider UserProvider { get; set; }

        protected RoleType GetRole(string projectID)
        {
            return this.ProjectProvider.GetRole(projectID, MvcApplication.CurrentUser.ID);
        }

        /// <summary>
        /// Ve el concepto en vista previa
        /// </summary>
        /// <param name="project">Proyecto</param>
        /// <param name="items">Elemento</param>
        /// <param name="type">Tipo de elemento</param>
        /// <returns></returns>
        public ActionResult Preview(Project project, ProjectItem[] items, ProjectItemType type)
        {
            if (this.GetRole(project.ID) != RoleType.Developer && !items.FirstOrDefault().IsPublic)
                return null;


            return View(new Model.View
            {
                Project = project,
                Item = items.FirstOrDefault(),
                Type = type,
                Role = this.ProjectProvider.GetRole(project.ID, MvcApplication.CurrentUser.ID)
            });
        }
        /// <summary>
        /// Resumen de vista previa (solo con conceptos que permitan multiplicidad)
        /// </summary>
        /// <param name="project">Proyecto</param>
        /// <param name="items">Elemento</param>
        /// <param name="type">Tipo de elemento</param>
        /// <returns></returns>
        public ActionResult PreviewResume(Project project, ProjectItem[] items, ProjectItemType type)
        {
            var role = this.ProjectProvider.GetRole(project.ID, MvcApplication.CurrentUser.ID);
            if (role != RoleType.Developer)
            {
                items = (from i in items
                         where i.IsPublic
                         select i).ToArray();
            }

            return View(new Model.Preview
            {
                Project = project,
                Items = items,
                Type = type,
                
            });
        }
        /// <summary>
        /// Vista de un concepto
        /// </summary>
        /// <param name="project">Proyecto</param>
        /// <param name="items">Elemento</param>
        /// <param name="type">Tipo de elemento</param>
        /// <returns></returns>
        public ActionResult View(string projectID, ProjectItemType type, string name)
        {
            var project = this.ProjectProvider.FindByID(projectID);
            var item = this.ProjectProvider.GetItem(projectID, type, name);

            return View(new Model.View
            {
                Project = project,
                Item = item,
                Type = type,
                Role = this.ProjectProvider.GetRole(project.ID, MvcApplication.CurrentUser.ID)
            });
        }
        /// <summary>
        /// Vista en arreglo de los conceptos
        /// </summary>
        /// <param name="project">Proyecto</param>
        /// <param name="items">Elemento</param>
        /// <param name="type">Tipo de elemento</param>
        /// <returns></returns>
        public ActionResult ViewArray(string projectID, ProjectItemType type, string name)
        {
            var project = this.ProjectProvider.FindByID(projectID);
            var role = this.ProjectProvider.GetRole(project.ID, MvcApplication.CurrentUser.ID);

            ProjectItem[] items = null;
            switch (type)
            {
                case ProjectItemType.Actors: items = project.Definition.Actors; break;
                case ProjectItemType.Objetives: items = project.Definition.Objetives; break;
                case ProjectItemType.ReqFunctionals: items = project.Requirements.Functionals; break;
                case ProjectItemType.ReqInformations: items = project.Requirements.Informations; break;
                case ProjectItemType.ReqNotFunctionals: items = project.Requirements.NotFunctionals; break;
            }

            if (role != RoleType.Developer)
            {
                items = (from i in items
                         where i.IsPublic
                         select i).ToArray();
            }
            var view = role == RoleType.Developer ? "ViewArray" : "ViewArrayLimited";
            return View(view, new Model.Preview
            {
                Project = project,
                Items = items,
                Type = type
            });

        }
        /// <summary>
        /// Nuevo concepto
        /// </summary>
        /// <param name="project">Proyecto</param>
        /// <param name="items">Elemento</param>
        /// <param name="type">Tipo de elemento</param>
        /// <returns></returns>
        public ActionResult New(string projectID, ProjectItemType type, string name)
        {
            var project = this.ProjectProvider.FindByID(projectID);
            this.ProjectProvider.SaveItem(projectID, type, new ProjectItem
            {
                Name = name
            });
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Indica si se permite su edición
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="name">Nombre</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult AllowEdit(string projectID, ProjectItemType type, string name)
        {
            var item = this.ProjectProvider.BlockItem(projectID, type, name, MvcApplication.CurrentUser);
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Guarda el concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="item">Elemento a guardar</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult SaveItem(string projectID, ProjectItemType type,string name, ProjectItem item)
        {
            var oldItem = this.ProjectProvider.GetItem(projectID, type, name);
            if (string.IsNullOrEmpty(item.Name))
                oldItem.Name = item.Name;
            oldItem.Data = item.Data;
            return Json(this.ProjectProvider.SaveItem(projectID, type, oldItem), JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Publica un concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="publish">Indica si debe publicar</param>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Publish(string projectID, ProjectItemType type, string name, bool publish)
        {
            return Json(this.ProjectProvider.PublishItem(projectID, type, name, publish), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Lista los comentarios
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <returns></returns>
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
        /// <summary>
        /// Vista de nuevo comentario
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <returns></returns>
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
        /// <summary>
        /// Ver comentario
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="timeTicks">Tiempo en que se ingresó</param>
        /// <returns></returns>
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

        /// <summary>
        /// Agregar nuevo comentario
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="message">Mensaje</param>
        /// <returns></returns>
        public ActionResult AddComment(string projectID, ProjectItemType type, string name, string message)
        {
            this.ProjectProvider.AddComment(projectID, type, name, message, MvcApplication.CurrentUser);
            return null;
        }
    }
}
