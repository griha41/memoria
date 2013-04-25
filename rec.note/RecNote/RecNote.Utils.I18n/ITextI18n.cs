using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;


namespace RecNote.Utils.I18n
{
    // Esta interface debe implementar adicionalmente IDiccionary<CultureInfo, IDicctionary<string, string>>
    public interface ITextI18n
    {
        void SetCulture(CultureInfo culture);
        string GetString(string code, CultureInfo culture);
        string GetString(string code);
        bool Contains(string key, CultureInfo culture);
        IDictionary<string, string> GetAll(CultureInfo culture);
        ICollection<string> Keys {get;}
        ICollection<string> Values { get; }
    }
}
