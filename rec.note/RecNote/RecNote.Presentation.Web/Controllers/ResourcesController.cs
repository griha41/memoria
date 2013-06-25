using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

namespace RecNote.Presentation.Web.Controllers
{
    public class ResourcesController : BaseController
    {
        //
        // GET: /Resources/
        public ActionResult I18n()
        {
            
            var i18n = Helpers.I18n.GetAll();

            Response.Write(JsonConvert.SerializeObject(i18n));
            Response.ContentType = "application/json";
            return null;
        }

    }
}
