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
        // TODO: Debe ser asignado a variable de guardado temporal y único por sesión
        public static User CurrentUser { get { return (User)HttpContext.Current.Items["CurrentUser"]; } set { HttpContext.Current.Items["CurrentUser"] = value; } }

        // Pais actual
        public static String CountryCode { get { return "es"; } }
    }
}