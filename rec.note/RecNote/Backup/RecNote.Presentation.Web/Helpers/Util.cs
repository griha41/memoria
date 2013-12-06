using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.WebPages;
using System.Text;
using RecNote.Presentation.Web.Models;
using System.Web.Mvc.Html;
using System.Linq.Expressions;

using System.Runtime.CompilerServices;
using System.Web.Mvc;

namespace RecNote.Presentation.Web.Helpers
{
    public static class Util
    {

        public static Dictionary<string, bool> FileExist = new Dictionary<string, bool>();

        public static string GetBaseUrl(this UrlHelper urlhelper)
        {
            var port = urlhelper.RequestContext.HttpContext.Request.Url.Port == 80 ? String.Empty : ":" + urlhelper.RequestContext.HttpContext.Request.Url.Port;
            return "http://" + urlhelper.RequestContext.HttpContext.Request.Url.Host + port + VirtualPathUtility.ToAbsolute("~/");
        }

        public static MvcHtmlString GetDynamicFiles(this UrlHelper urlhelper)
        {
            var views = ViewEngine.CurrentViews;
            var result = string.Empty;
            var lessResult = string.Empty;
            foreach (var view in views)
            {
                var match = Regex.Match(view, @"([\w]*\/[\w]*)\.cshtml");
                var index = views.IndexOf(view);
                var datetime = DateTime.UtcNow.ToString("ddhmmssff");
                if (match.Success)
                {
                    var script = Regex.Replace(match.Value, @"([\w]*\/[\w]*)\.cshtml", "~/Content/views/$1.js");
                    if (!FileExist.ContainsKey(script))
                        FileExist.Add(script, File.Exists(urlhelper.RequestContext.HttpContext.Server.MapPath(script)));
                    if (FileExist[script])
                        result += "<script type=\"text/javascript\" src=\"" + urlhelper.Content(script) + "?" + datetime + "\" ></script>";

                    var less = Regex.Replace(match.Value, @"([\w]*\/[\w]*)\.cshtml", "~/Content/views/$1.less");
                    if (!FileExist.ContainsKey(less))
                        FileExist.Add(less, File.Exists(urlhelper.RequestContext.HttpContext.Server.MapPath(less)));
                    if (FileExist[less])
                        lessResult += "var e" + index + " = $('<link type=\"text/css\" rel=\"stylesheet/less\" href=\"" + urlhelper.Content(less) + "?" + datetime + "\" />')[0];"
                            + "Util.addLess(e" + index + ");";
                }
            }
            
            if(!string.IsNullOrEmpty(lessResult))
            result += "<script type=\"text/javascript\" >"
                            + lessResult
                        +"</script>";


            var scriptBuilder = (StringBuilder)urlhelper.RequestContext.HttpContext.Items["ScriptBlockBuilder"] ?? new StringBuilder();
            result += scriptBuilder.ToString();

            return new MvcHtmlString(result);
        }
        public static bool IsAction(this UrlHelper urlhelper, string action, string controller)
        {
            return (urlhelper.RequestContext.RouteData.Values["action"] == action && urlhelper.RequestContext.RouteData.Values["controller"] == controller);
        }

        public static string FullAction(this UrlHelper urlhelper)
        {
            return (urlhelper.RequestContext.RouteData.Values["controller"] + "/" + urlhelper.RequestContext.RouteData.Values["action"]).ToLower();
        }

        public static MvcHtmlString ScriptBlock(this WebViewPage webPage, Func<dynamic, HelperResult> template)
        {
            var scriptBuilder = (StringBuilder)webPage.Context.Items["ScriptBlockBuilder"] ?? new StringBuilder();
            scriptBuilder.Append(template(null).ToHtmlString());
            webPage.Context.Items["ScriptBlockBuilder"] = scriptBuilder;
            return new MvcHtmlString(string.Empty);
        }

        public static MvcHtmlString DropDownListFor<T,TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, 
            Expression<Func<TModel, TProperty>> expression,
            
            T value) where T : struct
        {
            var items = new List<SelectListItem>();
            
            items.Add(new SelectListItem
            {
                Selected = (string.IsNullOrEmpty(value.ToString())  || value.ToString() == default(T).ToString()),
                Text = I18n.GetString(default(T)).ToString(),
                Value = string.Empty
            });

            items.AddRange(
            Enum.GetNames(typeof(T)).Select(p => new SelectListItem
            {
                Selected = (Enum.Parse(value.GetType(), p).Equals(value)),
                Text = I18n.GetString((T)Enum.Parse(value.GetType(), p)).ToString(),
                Value = p
            })
            );

            return htmlHelper.DropDownListFor<TModel,TProperty>(expression, items);
        }


    }
}