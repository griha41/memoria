using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using Spring.Context.Support;

namespace RecNote.Presentation.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : Models.Global
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              "i18n", // Route name
              "i18n.js", // URL with parameters
              new { controller = "Resources", action = "I18n", id = UrlParameter.Optional } // Parameter defaults
          );


            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

          
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DependencyResolver.SetResolver(new Models.DependencyResolver(ContextRegistry.GetContext()));
            var resolver = (Models.DependencyResolver)DependencyResolver.Current;


            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // Controladores vía spring
            FilterProviders.Providers.Clear();
            FilterProviders.Providers.Add((IFilterProvider)resolver.springContext.GetObject("FilterProvider"));


            // Se añade el controlador para páginas Jeronimo
            System.Web.Mvc.ViewEngines.Engines.Clear();
            System.Web.Mvc.ViewEngines.Engines.Add(new Models.ViewEngine());
        }
    }
}