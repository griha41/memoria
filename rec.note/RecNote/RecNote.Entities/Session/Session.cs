using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Entities.Session
{
    public class Session : Base
    {
        public Entities.Users.User User { get; set; }
        public DateTime Time { get; set; }
    }
}
