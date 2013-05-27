using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Domain.Core.Base.Session
{
    public class SessionProvider : ProviderBase<Entities.Session.Session>, RecNote.Domain.Core.Session.ISessionProvider
    {
        public Entities.Session.Session New(Entities.Users.User user)
        {
            return this.Save(new Entities.Session.Session
            {
                User = user,
                Time = DateTime.UtcNow
            });
        }
    }
}
