using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class InformationNode
    {
        public string Data { get; set; }
        
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public bool AllowChilds { get; set; }

        InformationNode[] Childs { get; protected set; }
    }
}
