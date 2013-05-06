using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;

namespace RecNote.Data.MongoDB
{
    public class Container<T>
    {
        public ObjectId Id { get; set; }
        public T Object { get; set; }

        public Container(T obj)
        {
            this.Object = obj;
        }
    }
}
