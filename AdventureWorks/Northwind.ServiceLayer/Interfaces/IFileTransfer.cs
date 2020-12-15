using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.ServiceLayer.Interfaces
{
    interface IFileTransfer
    {
        void Copy(FileStream file, string destination);
        void Move(FileStream file, string destination);
    }
}
