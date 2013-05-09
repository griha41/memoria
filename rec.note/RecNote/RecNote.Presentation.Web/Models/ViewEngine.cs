using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RecNote.Presentation.Web.Models
{
    public class ViewEngine : RazorViewEngine
    {
        public static List<string> CurrentViews
        {
            get
            {
                if (HttpContext.Current.Items["currentViews"] == null)
                    HttpContext.Current.Items["currentViews"] = new List<string>();
                return (List<string>)HttpContext.Current.Items["currentViews"];
            }
            set
            {
                HttpContext.Current.Items["currentViews"] = value;
            }
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            // Guardo un historial de las vistas generadas
            if (!CurrentViews.Exists(p => p == viewPath))
                CurrentViews.Add(viewPath);

            return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}