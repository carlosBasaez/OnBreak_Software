using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CacheLocal
{
    public class FileDicctionary
    {
        public string filePath = "";

        public FileDicctionary(string fileName)
        {
            this.filePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, fileName);
            Console.WriteLine("path: " + filePath);
        }

        public bool Exist => File.Exists(filePath);

        public void DiccToFile(Dictionary<string, string> dicc)
        {
            if (Exist)
            {
                File.Delete(filePath);
            }
            Console.WriteLine("Creating file");
            using (StreamWriter sw = File.CreateText(filePath))
            {
                foreach (var valores in dicc)
                {
                    sw.WriteLine(valores.Key + "=" + valores.Value);
                }
                sw.Close();
            }
        }

        public Dictionary<string, string> FileToDicc()
        {
            if (!Exist) return null;
            Dictionary<string, string> dicc = new Dictionary<string, string>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    dicc.Add(line.Split('=')[0], line.Split('=')[1]);
                }
            }

            return dicc;
        }

        public void LookDicc(Dictionary<string, string> dicc)
        {
            foreach (var valores in dicc)
            {
                Console.WriteLine(valores.Key + " = " + valores.Value);
            }

        }

        public void DeleteFile()
        {
            File.Delete(filePath);
        }
    }
}
