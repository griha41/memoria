using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audios;
using RecNote.Domain.Core.Projects;
using Model = RecNote.Presentation.Web.Models.Audio;

using Lib.Web.Mvc;

using System.Runtime;
using System.IO;

namespace RecNote.Presentation.Web.Controllers
{
    public class ReportController : BaseController
    {
        //
        // GET: /Audio/
        IProjectProvider ProjectProvider { get; set; }

        public ActionResult Project(string projectID)
        {

            var project = this.ProjectProvider.FindByID(projectID);

            Response.AppendHeader("content-disposition", "attachment;filename="  + project.Name + ".doc");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-word";

            return View("Project", project);
        }
        
    }
}
