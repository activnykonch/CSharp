using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FileWatcherService
{
    class EncryptFile
    {
        System.IO.FileInfo fileInfo;
        string key;

        public EncryptFile(string path)
        {
            fileInfo = new System.IO.FileInfo(path);
            key = "YungHefner";
        }

        public void Encrypt()
        {
            ;
        }

        public void Decrypt()
        {
            System.IO.StringReader stream = new System.IO.StringReader(fileInfo.FullName);
            if (fileInfo.Exists)
            {
                string str;
                string data = "";
                while ((str = stream.ReadLine()) != null)
                {
                    data += str;
                }
                stream.Close();
                List<string> content = new List<string>();
                //content = hash.
                System.IO.StreamWriter writer = new System.IO.StreamWriter(fileInfo.FullName, false);
                while (content.Count != 0)
                {
                    writer.WriteLine(content[0]);
                    content.RemoveAt(0);
                }
                writer.Close();
            }
        }
    }
}
