using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = RecNote.Presentation.Web.Models.Home;

using RecNote.Domain.Core.Users;
using RecNote.Domain.Core.Session;

namespace RecNote.Presentation.Web.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        IUserProvider UserProvider { get; set; }

        public ActionResult Index()
        {
            if (MvcApplication.CurrentUser == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        public ActionResult RecoveryPassword(string mail)
        {
            var user = this.UserProvider.FindByEmail(mail);
            if(user != null)
                this.UserProvider.NewPassword(user);
            return Json(true, JsonRequestBehavior.DenyGet);
        }

        public ActionResult Login(Model.Login model)
        {
            if (model == null) model = new Model.Login { };
            if (!string.IsNullOrEmpty(model.Username))
            {
                if (this.UserProvider.Login(model.Username, model.Password))
                {
                    MvcApplication.CurrentUser = this.UserProvider.FindByEmail(model.Username);
                    var session = this.SessionProvider.New(MvcApplication.CurrentUser);
                    Response.SetCookie(new HttpCookie("SessionID", session.ID));
                }
                
                return RedirectToAction("");
            }

            return View("Login", model);
        }

        public ActionResult Close()
        {
            MvcApplication.CurrentUser = null;
            Response.SetCookie(new HttpCookie("SessionID", "") { Expires = DateTime.Now.AddDays(-1) });
            return Json(true);
        }

    }
}
