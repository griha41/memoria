using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Files
{
    /// <summary>
    /// Comparador de archivos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="R"></typeparam>
    public interface IFileCompareProvider<T,R>
    {
        /// <summary>
        /// Compara el archivo 1 con el archivo dos con un margen de similaridad mínimo
        /// </summary>
        /// <param name="file1">Archivo 1</param>
        /// <param name="file2">Archivo 2</param>
        /// <param name="similarity">Similaridad mínima</param>
        /// <returns></returns>
        R Compare(T file1, T file2, float similarity);
    }
}
