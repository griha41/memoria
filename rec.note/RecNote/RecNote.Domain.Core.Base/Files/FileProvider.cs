using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using RecNote.Data.Files;

namespace RecNote.Domain.Core.Base.Files
{
    public class FileProvider : ProviderBase<Entities.Files.File>, Domain.Core.Files.IFileProvider
    {
        string PartialPath { get; set; }
        IFileData FileData { get; set; }
        public Entities.Files.File Upload(string name, string contentType, byte[] file)
        {
            var dirPath = Path.Combine(this.PartialPath, DateTime.UtcNow.ToString("yyyyMMddhhmmss"));
            if(!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);

            var fullPath = Path.Combine(dirPath, name);
            File.WriteAllBytes(fullPath, file);
            
            return this.FileData.Save(new Entities.Files.File
            {
                FullPath = fullPath,
                Name = name,
                Size = file.LongCount(),
                ContentType = contentType
            });
        }

        public byte[] GetFile(string fileID)
        {
            var file = this.FindByID(fileID);
            return File.ReadAllBytes(file.FullPath);
        }
    }
}
