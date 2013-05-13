using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class Actor : Users.User
    {
        public RoleType Role { get; set; }
    }
}
