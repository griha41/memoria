using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace RecNote.Domain.Core.Files.LocalServer
{
    public class File : Files.IFile
    {
        string PartialPath { get; set; }
        public string Upload(string name, byte[] file)
        {
            var fullName = Path.Combine(this.PartialPath, Path.GetFileName(name));
        }
    }
}
