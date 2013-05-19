using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Projects
{
    public class RestrictedProject
    {
        public RoleType Role { get; set; }
        public bool IsOwner { get; set; }
        public Project Project { get; set; }
        public Users.User UserAllow { get; set; }
    }
}
