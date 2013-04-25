using System;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RecNote.Utils.I18n.ResourcesFile
{
    public class TextI18n : ITextI18n
    {
        public string ResourcesFolder { get; set; }
        public CultureInfo Culture { get; set; }

        private static Dictionary<CultureInfo, CustomResourceManager> Resource = new Dictionary<CultureInfo, CustomResourceManager>();
        
        private string ResourceFile(CultureInfo culture)
        {
            var file = this.ResourcesFolder + culture.Name + ".resx";
            return (System.IO.File.Exists(file)) ? file : this.ResourcesFolder + culture.TwoLetterISOLanguageName + ".resx";
        }

        private void Check()
        {
            if (this.Culture == null)
                throw new Exception("Culture no seted");
            if (!Resource.ContainsKey(this.Culture))
                Resource.Add(this.Culture, new CustomResourceManager(this.ResourceFile(this.Culture)));
        }


        public void SetCulture(CultureInfo culture)
        { this.Culture = culture; }

        public bool Exists(string code)
        {
            return Resource[this.Culture].Contains(code);
        }

        public string GetString(string code)
        {
            this.Check();
            return Resource[this.Culture].GetString(code);
        }

        public string GetString(string code, CultureInfo culture)
        {
            if (this.Culture == null) this.Culture = culture;
            if (!Resource.ContainsKey(culture)) Resource.Add(culture, new CustomResourceManager(this.ResourceFile(culture)));
            return (Resource[culture].Contains(code)) ? Resource[culture][code].ToString() : code;
        }

        
        public System.Collections.IDictionaryEnumerator GetEnumerator()
        {
            return Resource[this.Culture].GetEnumerator();
        }
        
      

        public int Count
        {
            get { return Resource[this.Culture].Count; }
        }

        public bool IsFixedSize { get { return false; } }

        public bool IsReadOnly { get { return true; } }

        public ICollection<string> Keys { get { return Resource[this.Culture].Keys; } }

        public ICollection<string> Values { get { return Resource[this.Culture].Values; } }


        public object this[object key] { get { return this[key]; } set { this[key] = value; } }

        public void Add(object key, object value) { throw new Exception("Add not allow"); }

        public void Clear() { throw new Exception("Clear not allow"); }

        public void CopyTo(Array array, int index) { throw new Exception("CopyTo not allow"); }

        public bool Contains(string key, CultureInfo culture) { return Resource[culture].Contains(key); }

        public bool Contains(object key) { return Resource[this.Culture].Contains(key.ToString()); }

        public void Remove(object key)
        {
            throw new Exception("Clear not allow");
        }
        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { return null; } }

        public IDictionary<string, string> GetAll(CultureInfo culture)
        {
            return Resource[culture];
        }
    }
}
