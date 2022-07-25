using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheLocal
{
    public class CacheDicctionary
    {
        /*public string fileName {
            get { return fileName; }
            private set { fileName = value; }
        }*/
        public string fileName;
        /*public string filePath {
            get { return filePath; }
            private set { filePath = value; }
        }*/
        public string filePath;
        FileDicctionary fileDicctionary;
        Dictionary<string, string> dicctionary = new Dictionary<string, string>();

        public CacheDicctionary(string fileName)
        {
            fileDicctionary = new FileDicctionary(fileName);
            this.fileName = fileName;
            this.filePath = fileDicctionary.filePath;
            if (fileDicctionary.Exist) Download(false);
        }

        public void Save()
        {
            fileDicctionary.DiccToFile(dicctionary);
        }

        public void Delete()
        {
            fileDicctionary.DeleteFile();
        }

        public void Download(bool additive = true)
        {
            if (!additive)
                dicctionary = fileDicctionary.FileToDicc();
            else
            {
                Dictionary<string, string> dicc2 = fileDicctionary.FileToDicc();
                foreach (var item in dicc2)
                {
                    if (!dicctionary.ContainsKey(item.Key))
                    {
                        dicctionary.Add(item.Key, item.Value);
                    }
                }
            }
        }

        public void Add(string key, string value)
        {
            dicctionary.Add(key, value);
        }

        public void Remove(string key)
        {
            dicctionary.Remove(key);
        }

        public bool ExistKey(string key) => dicctionary.ContainsKey(key);

        public string Get(string key)
        {
            if (dicctionary.TryGetValue(key, out string value))
                return value;
            return null;
        }

        public Dictionary<string, string> GetAll()
        {
            return new Dictionary<string, string>(dicctionary);
        }

        public void Clear()
        {
            dicctionary.Clear();
        }

        public bool ExistFile()
        {
            return fileDicctionary.Exist;
        }

    }
}
