using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using RecNote.Entities.Users;

namespace RecNote.Presentation.Web.Helpers
{
    public abstract class CurrentData
    {
        public static User CurrentUser { get { return MvcApplication.CurrentUser; } }
    }
}