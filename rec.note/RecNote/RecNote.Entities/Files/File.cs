using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace RecNote.Entities.Files
{
    
    public class File : Entities.Base
    {
        
        public string FullPath { get; set; }
        
        public string Name { get; set; }
        public string Url { get; set; }
        public long Size { get; set; }
        public string ContentType { get; set; }
    }
}
