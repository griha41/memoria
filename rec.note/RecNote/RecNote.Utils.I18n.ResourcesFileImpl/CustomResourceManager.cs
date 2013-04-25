using System;
using System.Web;
using System.Resources;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;

namespace RecNote.Utils.I18n.ResourcesFile
{
    class CustomResourceManager : IDictionary<string, string>
    {
        Dictionary<string, string> Elements;

        public ICollection<string> Keys { get { return this.Elements.Keys; } }

        public ICollection<string> Values { get { return this.Elements.Values; } }

        public CustomResourceManager(string path)
        {
            if (path.IndexOf(@"~") > -1)
                path = HttpContext.Current.Server.MapPath(path);
            
            var reader = new ResXResourceReader(@path);
            this.Elements = new Dictionary<string,string>();
            var num = reader.GetEnumerator();
            while (num.MoveNext())
                this.Elements.Add(num.Key.ToString(), num.Value.ToString());
        }
        

        public string GetString(string codeDialog)
        {
            return this.Elements.ContainsKey(codeDialog) ? this.Elements[codeDialog] : string.Empty;
        }

        public IDictionaryEnumerator GetEnumerator()
        {
            return this.Elements.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, string>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string, string>>.GetEnumerator()
        {
            return ((System.Collections.Generic.IEnumerable<KeyValuePair<string, string>>)this.Elements).GetEnumerator();
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((System.Collections.IEnumerable)this.Elements).GetEnumerator();
        }
        
        
        public int Count
        {
            get { return this.Elements.Count; }
        }

        public bool IsFixedSize { get { return false; } }

        public bool IsReadOnly { get { return true; } }

        public string this[string key] { get { return this.Elements[key]; } set { this.Elements[key] = value; } }

        public void Add(string key, string value)
        {
            throw new Exception("Add not allow");
        }

        public void Add(KeyValuePair<string, string> value)
        {
            throw new Exception("Add not allow");
        }

        public void Clear()
        {
            throw new Exception("Clear not allow");
        }

        public bool Contains(string key)
        {
            return this.Elements.ContainsKey(key);
        }
        public bool Contains(KeyValuePair<string, string> pair)
        {
            throw new NotImplementedException();
        }
        public bool ContainsKey(string key)
        {
            return Contains(key);
        }

        public bool Remove(string key)
        {
            throw new Exception("Clear not allow");
        }

        public bool Remove(KeyValuePair<string, string> key)
        {
            throw new Exception("Clear not allow");
        }

        public void CopyTo(KeyValuePair<string,string>[] array, int index)
        {
            throw new Exception("Copy not allow");
        }

        public bool IsSynchronized { get { return true; } }

        public object SyncRoot { get { return null; } }

        public bool TryGetValue(string key, out string value)
        {
            if (this.ContainsKey(key))
                value = this[key];
            else
                value = null;
            return this.ContainsKey(key);
        }

    }
}
