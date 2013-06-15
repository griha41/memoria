using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Entities.Audios;
using Model = RecNote.Presentation.Web.Models.Audio;

namespace RecNote.Presentation.Web.Controllers
{
    public class AudioController : Controller
    {
        //
        // GET: /Audio/
        
        

        public ActionResult List(string projectID)
        {
            return View();
        }

        public ActionResult New(string projectID)
        {
            return View(new Model.New
            {
                ProjectID = projectID,
                Audio = new Audio()
            });
        }

    }
}
