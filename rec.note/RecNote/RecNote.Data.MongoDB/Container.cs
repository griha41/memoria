using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;

namespace RecNote.Data.MongoDB
{
    public class Container<T> where T : Entities.Base
    {
        public ObjectId Id { get; set; }
        public T Object { get; set; }

        public Container(T obj)
        {
            this.Object = obj;
        }

        public static implicit operator T(Container<T> obj)
        {
            if (obj == null) return null;
            var e = obj.Object;
            e.ID = obj.Id.ToString();
            return e;
        }

        public static implicit operator Container<T>(T obj)
        {
            return new Container<T>(obj);
        }

    }
}
