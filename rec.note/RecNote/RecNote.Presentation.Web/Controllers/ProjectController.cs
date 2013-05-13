﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Domain.Core.Projects;
using Model = RecNote.Presentation.Web.Models.Project;

namespace RecNote.Presentation.Web.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/
        IProjectProvider ProjectProvider { get; set; }

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult List()
        {
            return null;
        }

        public ActionResult New(Entities.Projects.Project project)
        {    
            var model = project.ID == null ? this.ProjectProvider.GetTemporalProject(MvcApplication.CurrentUser) : project;
            return View(model);
        }

        public ActionResult NewActor(Model.Actor actor)
        {
            var model = actor ?? new Model.Actor {
                ProjectID = this.ProjectProvider.GetTemporalProject(MvcApplication.CurrentUser).ID
            };
            return View(model);
        }

        public ActionResult SaveActor(Model.Actor actor)
        {
            this.ProjectProvider.NewActor(actor.ProjectID, actor);
            return null;
        }

        public ActionResult ListActors(string projectID)
        {
            var project = this.ProjectProvider.FindByID(projectID);
            return null;
        }
    }
}
