using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Domain.Core.Projects;
using RecNote.Entities.Projects;
using Model = RecNote.Presentation.Web.Models.Project;

namespace RecNote.Presentation.Web.Controllers
{
    /// <summary>
    /// Controlador de proyecto
    /// </summary>
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/
        IProjectProvider ProjectProvider { get; set; }

        protected RoleType GetRole(string projectID)
        {
            return this.ProjectProvider.GetRole(projectID, MvcApplication.CurrentUser.ID);
        }

        /// <summary>
        /// Listado de proyectos
        /// </summary>
        /// <returns></returns>
        public ActionResult Menu()
        {
            return View();
        }
        /// <summary>
        /// Listado de proyectos por usuario
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            var projects = new Model.List { RestrictedProjects = this.ProjectProvider.GetProjectByUser(MvcApplication.CurrentUser.ID) };
            return View(projects);
        }
        /// <summary>
        /// Pantalla de nuevo proyecto
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ActionResult New(Model.Project project)
        {
            var model = project.CurrentProject == null ? new Model.Project { CurrentProject = this.ProjectProvider.GetTemporalProject(MvcApplication.CurrentUser) } : project;
            return View(model);
        }
        /// <summary>
        /// Servicio de agregación de actor
        /// </summary>
        /// <param name="actor">actor a agregar</param>
        /// <returns></returns>
        public ActionResult NewActor(Model.Actor actor)
        {
            var model = actor ?? new Model.Actor {
                ProjectID = this.ProjectProvider.GetTemporalProject(MvcApplication.CurrentUser).ID
            };
            return View(model);
        }
        /// <summary>
        /// Guarda actor en el proyecto
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public ActionResult SaveActor(Model.Actor actor)
        {
            this.ProjectProvider.NewActor(actor.ProjectID, actor.User);
            return null;
        }
        /// <summary>
        /// Borra el actor del proyecto
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public ActionResult RemoveActor(Model.Actor actor)
        {
            this.ProjectProvider.RemoveActor(actor.ProjectID, actor.User.ID);
            return null;
        }
        /// <summary>
        /// Lista actores del proyecto
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ActionResult ListActors(string projectID)
        {
            var project = this.ProjectProvider.FindByID(projectID);
            if (this.GetRole(projectID) == RoleType.Developer)
                return View(project);
            else
                return View("ListActorsLimited", project);
        }
        /// <summary>
        /// Guarda el proyecto
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public ActionResult Save(Model.Project project)
        {
            var realProject = this.ProjectProvider.FindByID(project.CurrentProject.ID);
            realProject.Name = project.CurrentProject.Name;
            this.ProjectProvider.Save(realProject);
            return null;
        }
        /// <summary>
        /// Vista general del proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <returns></returns>
        public new ActionResult View(string projectID)
        {
            if(this.GetRole(projectID) == RoleType.Developer)
                return View(new Model.Project { CurrentProject = this.ProjectProvider.FindByID(projectID) });
            else
                return View("ViewLimited", new Model.Project { CurrentProject = this.ProjectProvider.FindByID(projectID) });
        }
        /// <summary>
        /// Lista de miembros del proyectos
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public ActionResult ListMembers(string projectID)
        {
            if (this.GetRole(projectID) == RoleType.Developer)
                return View(new Model.Project { CurrentProject = this.ProjectProvider.FindByID(projectID) });
            else
                return View("ListMembersLimited", new Model.Project { CurrentProject = this.ProjectProvider.FindByID(projectID) });
        }
    }
}
