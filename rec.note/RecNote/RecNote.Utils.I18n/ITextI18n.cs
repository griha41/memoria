using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;


namespace RecNote.Utils.I18n
{
    // Esta interface debe implementar adicionalmente IDiccionary<CultureInfo, IDicctionary<string, string>>
    /// <summary>
    /// Interface de obtención de dialogo por cultura
    /// </summary>
    public interface ITextI18n
    {
        /// <summary>
        /// Configura la cultura por defecto
        /// </summary>
        /// <param name="culture">Cultura</param>
        void SetCulture(CultureInfo culture);
        /// <summary>
        /// Obtiene un dialogo
        /// </summary>
        /// <param name="code">Código</param>
        /// <param name="culture">Cultura deseada</param>
        /// <returns></returns>
        string GetString(string code, CultureInfo culture);
        /// <summary>
        /// Obtiene un dialogo con la cultura por defecto
        /// </summary>
        /// <param name="code">Código</param>
        /// <returns></returns>
        string GetString(string code);
        /// <summary>
        /// Indica si existe el código en la cultura indicada
        /// </summary>
        /// <param name="key">Código</param>
        /// <param name="culture">Cultura</param>
        /// <returns></returns>
        bool Contains(string key, CultureInfo culture);
        /// <summary>
        /// Obtiene todos los dialogos de una cultura
        /// </summary>
        /// <param name="culture">Cultura</param>
        /// <returns></returns>
        IDictionary<string, string> GetAll(CultureInfo culture);
        /// <summary>
        /// Obtiene todos los códigos de la cultura por defecto
        /// </summary>
        ICollection<string> Keys {get;}
        /// <summary>
        /// Obtiene todos los valores de la cultura por defecto
        /// </summary>
        ICollection<string> Values { get; }
    }
}
