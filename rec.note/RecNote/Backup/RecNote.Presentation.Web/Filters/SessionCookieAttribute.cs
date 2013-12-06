using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using RecNote.Domain.Core.Session;

namespace RecNote.Presentation.Web.Filters
{
    public class SessionCookieAttribute : ActionFilterAttribute
    {
        protected ISessionProvider SessionProvider { get; set; }
        protected string SessionName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var Request = HttpContext.Current.Request;
            if (Request.Cookies.AllKeys.Contains(this.SessionName))
            {
                var session = this.SessionProvider.FindByID(Request.Cookies[this.SessionName].Value);
                if (session != null)
                    MvcApplication.CurrentUser = session.User;
            }
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}