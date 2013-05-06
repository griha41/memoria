﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Domain.Core.Users;
using RecNote.Entities.Users;

using RecNote.Data.Users;

namespace RecNote.Domain.Core.Base.Users
{
    class UserProvider : ProviderBase<User>, IUserProvider
    {
        public IUserData UserData { get; set; }

        private string emailAdmin { get; set; }
        private string passwordAdmin { get; set; }

        public bool Login(string email, string password)
        {
            if (email.Equals(this.emailAdmin) && password.Equals(this.passwordAdmin)) return true;
            
            return false;
        }
    }
}
