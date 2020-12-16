using System;
using System.Collections.Generic;

namespace Northwind.DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void LogError(Exception exception);
    }
}