using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;

using Spring.Context;


namespace RecNote.Presentation.Web.Models
{
    public class DependencyResolver : IDependencyResolver
    {
        
        public IApplicationContext springContext { get; set; }

        public DependencyResolver(IApplicationContext context)
        {
            springContext = context;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType != null)
            {
                var services = springContext.GetObjectsOfType(serviceType);
                if (services.Count > 0)
                {
                    return services.Cast<DictionaryEntry>().First().Value;
                }
            }
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            IDictionary dictionary = springContext.GetObjectsOfType(serviceType);
            return dictionary.Values.Cast<object>();
        }
    }
}