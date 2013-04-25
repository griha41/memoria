using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using RecNote.Entities.Users;

namespace RecNote.Presentation.Web.Models
{
    public class Global : System.Web.HttpApplication
    {
        public static HttpSessionState Session { get { return HttpContext.Current.Session; } }

        // TODO: Debe ser asignado a variable de guardado temporal y único por sesión
        public static User CurrentUser { get { return (User)Session["CurrentUser"]; } set { Session["CurrentUser"] = value; } }

        // Pais actual
        public static String CountryCode { get { return "es"; } }
    }
}