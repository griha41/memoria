using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Session
{
    public interface ISessionProvider
    {
        Entities.Session.Session New(Entities.Users.User user);
        Entities.Session.Session FindByID(string id);
    }
}
