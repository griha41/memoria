using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model = RecNote.Presentation.Web.Models.Home;

using RecNote.Domain.Core.Users;

namespace RecNote.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        IUserProvider UserProvider { get; set; }
        int Number { get; set; }

        public ActionResult Index()
        {
            if (MvcApplication.CurrentUser == null)
                return RedirectToAction("Login", "Home");

            return View();
        }

        public ActionResult Login(Model.Login model)
        {
            if (model == null) model = new Model.Login { };
            if (!string.IsNullOrEmpty(model.Username))
            {
                MvcApplication.CurrentUser = new Entities.Users.User
                {
                    Name = model.Username,
                    Email = model.Username
                };
                return RedirectToAction("");
            }

            return View("Login", model);
        }

        public ActionResult AddUser(Model.Login model)
        {

            var e = this.UserProvider.Save(new Entities.Users.User {Name = model.Username, Email = model.Username });
            return null;
        }

    }
}
