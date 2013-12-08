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


        public Entities.Files.File[] FindByType(string type)
        {
            return this.FileData.FindByType(type);
        }


        public TF FindByID<TF>(string fileID) where TF : Entities.Files.File
        {
            return this.GenericDataBase.FindByID<Entities.Files.File, TF>(fileID);
        }


        public byte[] GetFile(string fileID, int offset, int count)
        {
            var file = this.FindByID(fileID);
            var bytes = new byte[count];
            var f = File.OpenRead(file.FullPath);
            f.Read(bytes,offset, count);
            return bytes;
        }

        public override Entities.Files.File Save(Entities.Files.File entry)
        {
            return this.FileData.Save(entry);
        }

        public override Entities.Files.File FindByID(string id)
        {
            return this.FileData.FindByID(id);
        }
    }
}
