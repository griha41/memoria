using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecNote.Presentation.Web.Controllers
{
    public class ProjectController : Controller
    {
        //
        // GET: /Project/

        public ActionResult Menu()
        {
            return View();
        }

        public ActionResult List()
        {
            return null;
        }

    }
}
