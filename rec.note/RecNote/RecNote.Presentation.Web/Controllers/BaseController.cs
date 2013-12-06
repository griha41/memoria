using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Domain.Core.Session;
using RecNote.Utils.I18n;

namespace RecNote.Presentation.Web.Controllers
{
    [Filters.SessionCookie]
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected ISessionProvider SessionProvider { get; set; }
        protected ITextI18n I18n { get; set; }
        protected string SessionName { get; set; }

    }
}
