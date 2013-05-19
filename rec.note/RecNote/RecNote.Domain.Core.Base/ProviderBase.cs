﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data;

namespace RecNote.Domain.Core.Base
{
    public abstract class ProviderBase<T> : IProviderBase<T> where T : Entities.Base
    {
        protected IGenericDataBase GenericDataBase { get; set; }

        public virtual T FindByID(string id) 
        {
            return this.GenericDataBase.FindByID<T>(id);
        }

        public virtual bool Remove(string id)
        {
            return this.GenericDataBase.Remove<T>(id);
        }

        public virtual T Save(T entry)
        {
            return this.GenericDataBase.Save<T>((T)entry);
        }
    }
}
