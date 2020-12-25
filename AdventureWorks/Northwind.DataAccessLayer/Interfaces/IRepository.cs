using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        void LogError(Exception exception);
        Task LogErrorAsync(Exception exception);
    }
}