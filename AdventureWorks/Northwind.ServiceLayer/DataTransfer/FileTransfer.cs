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
                using(FileStream stream = File.Create(destination))
                {
                    file.CopyTo(stream);
                }
            }
            else throw new Exception("FileError: Destination file already exists");
        }

        public void Move(FileStream file, string destination)
        {
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
            {
                using (FileStream stream = File.Create(destination))
                {
                    File.Move(file.Name, stream.Name);
                }
            }
            else throw new Exception("FileError: Destination file already exists");
        }
    }
}
