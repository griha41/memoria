using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Files
{
    public interface IFileCompareProvider<T,R>
    {
        R Compare(T file1, T file2, float similarity);
    }
}
