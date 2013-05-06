using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spring.Context;
using Spring.Context.Support;

namespace RecNote.Utils.MvcExtensions.Providers
{
    class FilterProvider : FilterAttributeFilterProvider, IApplicationContextAware
    {
        public IApplicationContext ApplicationContext { get; set; }


        public override IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(controllerContext, actionDescriptor);
            foreach (var filter in filters)
            {
                if (ApplicationContext.ContainsObject(filter.Instance.GetType().Name))
                {
                    yield return new Filter(ApplicationContext.GetObject(filter.Instance.GetType().Name), filter.Scope, filter.Order);
                }
                else
                {
                    yield return filter;
                }
            }
        }
    }
}
