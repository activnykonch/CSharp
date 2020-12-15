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
<<<<<<< HEAD
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
=======
<<<<<<< HEAD
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
            {
                file.CopyTo(stream);
            }
=======
            if (!File.Exists(destination))
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
            {
                using(FileStream stream = File.Create(destination))
                {
                    file.CopyTo(stream);
                }
            }
            else throw new Exception("FileError: Destination file already exists");
<<<<<<< HEAD
=======
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
        }

        public void Move(FileStream file, string destination)
        {
<<<<<<< HEAD
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
=======
<<<<<<< HEAD
            string path = Path.Combine(destination, Path.GetFileName(file.Name));
            using (FileStream stream = File.Create(path))
            {
                File.Move(file.Name, stream.Name);
            }
=======
            if (!File.Exists(destination))
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
            {
                using (FileStream stream = File.Create(destination))
                {
                    File.Move(file.Name, stream.Name);
                }
            }
            else throw new Exception("FileError: Destination file already exists");
<<<<<<< HEAD
=======
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
>>>>>>> ed88f2039df0f11b9548e3465822d9aad58615c8
        }
    }
}
