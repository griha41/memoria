using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver.Builders;

namespace RecNote.Data.MongoDB.Users
{
    class UserData : DataBase<Entities.Users.User>,  Data.Users.IUserData
    {

        public Entities.Users.User FindByEmail(string email)
        {
            return this.GetCollection().Find(Query<Container<Entities.Users.User>>.EQ(p => p.Object.Email, email)).FirstOrDefault();
        }
    }
}
