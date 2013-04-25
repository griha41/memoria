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

namespace RecNote.Presentation.Web.Helpers
{
    public static class Util
    {

        public static Dictionary<string, bool> FileExist = new Dictionary<string, bool>();
        public static MvcHtmlString GetDynamicFiles(this UrlHelper urlhelper)
        {
            var views = ViewEngine.CurrentViews;
            var result = "";
            foreach (var view in views)
            {
                var match = Regex.Match(view, @"([\w]*\/[\w]*)\.cshtml");
                if (match.Success)
                {
                    var script = Regex.Replace(match.Value, @"([\w]*\/[\w]*)\.cshtml", "~/Content/views/$1.js");
                    if (!FileExist.ContainsKey(script))
                        FileExist.Add(script, File.Exists(urlhelper.RequestContext.HttpContext.Server.MapPath(script)));
                    if (FileExist[script])
                        result += "<script type=\"text/javascript\" src=\"" + urlhelper.Content(script) + "\" ></script>";

                    var less = Regex.Replace(match.Value, @"([\w]*\/[\w]*)\.cshtml", "~/Content/views/$1.less");
                    if (!FileExist.ContainsKey(less))
                        FileExist.Add(less, File.Exists(urlhelper.RequestContext.HttpContext.Server.MapPath(less)));
                    if (FileExist[less])
                        result += "<link type=\"text/css\" rel=\"stylesheet/less\" href=\"" + urlhelper.Content(less) + "\" ></script>";
                }
            }
            var port = urlhelper.RequestContext.HttpContext.Request.Url.Port == 80 ? String.Empty : ":" + urlhelper.RequestContext.HttpContext.Request.Url.Port;
            string urlBase = "http://" + urlhelper.RequestContext.HttpContext.Request.Url.Host + port + VirtualPathUtility.ToAbsolute("~/");
            result += "<script type=\"text/javascript\" >window.baseUrl = '" + urlBase + "';</script>";


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


    }
}