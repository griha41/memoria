using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Files;

namespace RecNote.Domain.Core.Files
{
    public interface IFileProvider : IProviderBase<Entities.Files.File>
    {
        File Upload(string name, string contentType, byte[] file);
        Byte[] GetFile(string fileID);
    }
}
