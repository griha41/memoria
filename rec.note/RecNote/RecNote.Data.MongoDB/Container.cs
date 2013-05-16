using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;

namespace RecNote.Data.MongoDB
{
    public class ContainerS<T> where T : Entities.Base
    {
        public ObjectId Id { get; set; }
        public T Object { get; set; }

        public ContainerS(T obj)
        {
            this.Object = obj;
        }

        

    }
}
