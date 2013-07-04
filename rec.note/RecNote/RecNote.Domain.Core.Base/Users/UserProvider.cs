using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Domain.Core.Users;
using RecNote.Entities.Users;

using RecNote.Data.Users;
using RecNote.Services.Mailing;

using System.Security.Cryptography;

namespace RecNote.Domain.Core.Base.Users
{
    class UserProvider : ProviderBase<User>, IUserProvider
    {
        IUserData UserData { get; set; }
        ISender Sender { get; set; }

        private string emailAdmin { get; set; }
        private string passwordAdmin { get; set; }
        private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

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

            if (user.Password == this.md5(password) ) return true;

            return false;
        }

        public User FindByEmail(string email)
        {
            return this.UserData.FindByEmail(email);
        }

        public User New(User User)
        {
            var user = this.FindByEmail(User.Email);
            if (user != null)
                User.ID = user.ID;

            if (string.IsNullOrEmpty(user.Password))
            {
                var password = this.random();
                user.Password = this.md5(password);
                this.Sender.Send(new System.Net.Mail.MailAddress("noreply@recnote.cl", "RecNote")
                    , new System.Net.Mail.MailAddress(user.Email, user.Name),
                    I18n.GetString("mail.password"), ContentType.Text, I18n.GetString("mail.passwordMessage").Replace("${password}", password));
            }

            return this.Save(User);
        }

        public void NewPassword(User user)
        {
            if (string.IsNullOrEmpty(user.Password))
            {
                var password = this.random();
                user.Password = this.md5(password);
                this.Sender.Send(new System.Net.Mail.MailAddress("noreply@recnote.cl", "RecNote")
                    , new System.Net.Mail.MailAddress(user.Email, user.Name),
                    I18n.GetString("mail.password"), ContentType.Text, I18n.GetString("mail.passwordMessage").Replace("${password}", password));
            }
            this.Save(user);
        }

        private string md5(string text)
        {
            var result = string.Empty;
            using (MD5 md5Hash = MD5.Create())
            {
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                result = string.Join("", data.Select(p => p.ToString("x2").ToLower()));
            }
            return result;
        }

        public string random()
        {
            var r = new Random();
            return new string(
                Enumerable.Repeat(chars, 8).Select( s => s[r.Next(s.Length)] ).ToArray()
                );
        }

    }
}
