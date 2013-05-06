using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Users
{
    public class User : Base
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
