using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



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
            return null;
        }

    }
}
