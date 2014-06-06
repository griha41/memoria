using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audio;
using RecNote.Domain.Core.Projects;
using Model = RecNote.Presentation.Web.Models.Audio;

using Lib.Web.Mvc;

using System.Runtime;
using System.IO;

namespace RecNote.Presentation.Web.Controllers
{
    /// <summary>
    /// Controla los reportes
    /// </summary>
    public class ReportController : BaseController
    {
        //
        // GET: /Audio/
        IProjectProvider ProjectProvider { get; set; }

        /// <summary>
        /// Permite la descarga de la exportación de la información
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
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
