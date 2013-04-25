using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

using RecNote.Utils.I18n;


using Spring.Context;
using Spring.Context.Support;

using System.Web.Routing;

using System.Text.RegularExpressions;

namespace RecNote.Presentation.Web.Helpers
{
    public static class I18n
    {
        private static readonly IApplicationContext ctx = ContextRegistry.GetContext();
        private static ITextI18n TextI18n { get { return (ITextI18n)ctx.GetObject("TextI18n"); } }

        private static Dictionary<string, CultureInfo> Cultures { get; set; }

        private static string GetTextString(string key)
        {
            
            if (string.IsNullOrEmpty(key)) return string.Empty;
            if (Cultures == null) Cultures = new Dictionary<string, CultureInfo>();
            // TODO: Debe venir del objeto el idioma que usa el país
            if (!Cultures.ContainsKey(MvcApplication.CountryCode))
                Cultures.Add(MvcApplication.CountryCode, new CultureInfo("es-" + MvcApplication.CountryCode));

            return TextI18n.GetString(key, Cultures[MvcApplication.CountryCode]);
        }

        public static IDictionary<string, string> GetAll()
        {
            if (Cultures == null) Cultures = new Dictionary<string, CultureInfo>();
            if (!Cultures.ContainsKey(MvcApplication.CountryCode))
                Cultures.Add(MvcApplication.CountryCode, new CultureInfo("es-" + MvcApplication.CountryCode));

            return TextI18n.GetAll(Cultures[MvcApplication.CountryCode]);
        }

        public static bool Contains(string key)
        {
            if (Cultures == null) Cultures = new Dictionary<string, CultureInfo>();
            if (!Cultures.ContainsKey(MvcApplication.CountryCode))
                Cultures.Add(MvcApplication.CountryCode, new CultureInfo("es-" + MvcApplication.CountryCode));

            return TextI18n.Contains(key, Cultures[MvcApplication.CountryCode]);
        }

        public static MvcHtmlString GetString(string key)
        {
            return MvcHtmlString.Create(GetTextString(key));
        }

        public static MvcHtmlString GetString<T>(T enu)
        {
            return GetString(typeof(T).Name + "." + enu);
        }

        public static MvcHtmlString GetString<T>(T? enu) where T : struct
        {
            if (enu.HasValue)
                return GetString(enu.Value);
            else
                return MvcHtmlString.Create(string.Empty);
        }

        public static Regex GetRegExp<T>(T enu)
        {
            return new Regex(GetTextString("regexp." + typeof(T).Name + "." + enu));
        }

        public static Regex GetRegExp<T>(T? enu) where T : struct
        {
            return enu.HasValue ? GetRegExp(enu.Value) : new Regex(string.Empty);
        }
    }
}