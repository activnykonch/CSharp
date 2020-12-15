using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Northwind.ServiceLayer.Interfaces;

namespace Northwind.ServiceLayer.DataTransfer
{
    public class FileTransfer : IFileTransfer
    {
        public FileTransfer()
        {

        }

        public void Copy(FileStream file, string destination)
        {
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
            {
                file.CopyTo(stream);
            }
        }

        public void Move(FileStream file, string destination)
        {
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
            {
                File.Move(file.Name, stream.Name);
            }
        }
    }
}
