using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Users;

namespace RecNote.Domain.Core.Users
{
    public interface IUserProvider : IProviderBase<User>
    {
        bool Login(string email, string password);
        User FindByEmail(string email);
        User New(User user);
        void NewPassword(User user);
    }
}
