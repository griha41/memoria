﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class InformationNode : Base
    {
        public string Data { get; set; }
        
        public bool IsPublic { get; set; }
        public string Name { get; set; }
        public bool AllowChilds { get; set; }

        public InformationNode[] Childs { get; set; }
    }
}
