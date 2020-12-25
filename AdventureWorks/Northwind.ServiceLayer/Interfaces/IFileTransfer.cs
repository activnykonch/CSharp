using System.IO;
using System.Threading.Tasks;

namespace Northwind.ServiceLayer.Interfaces
{
    interface IFileTransfer
    {
        void Copy(FileStream file, string destination);
        void Move(FileStream file, string destination);
        Task CopyAsync(FileStream file, string destination);
        Task MoveAsync(FileStream file, string destination);
    }
}
