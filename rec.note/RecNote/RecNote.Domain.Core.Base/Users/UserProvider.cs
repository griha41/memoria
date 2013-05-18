using System;
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
            if (email.Equals(this.emailAdmin) && password.Equals(this.passwordAdmin))
            {
                if (this.FindByEmail(email) == null)
                {
                    this.Save(new User
                    {
                        Email = this.emailAdmin,
                        Password = this.passwordAdmin,
                        Name = "admin"
                    });
                }
                return true;
            }
            var user = this.FindByEmail(email);
            if (user == null) return false;

            // TODO: Debe ser pasada a un hashing la password
            if (user.Password == password) return true;

            return false;
        }

        public User FindByEmail(string email)
        {
            return this.UserData.FindByEmail(email);
        }

        public User New(User User)
        {
            // TODO: Enviar el password al usuario por correo electronico
            var user = this.FindByEmail(User.Email);
            if (user != null)
                User.ID = user.ID;

            return this.Save(User);
        }

    }
}
