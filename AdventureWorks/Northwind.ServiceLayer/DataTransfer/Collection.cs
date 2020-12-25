using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Northwind.DataAccessLayer.Interfaces;
using Northwind.DataAccessLayer.Repositories;
using Northwind.Models;

namespace Northwind.ServiceLayer.DataTransfer
{
    public class Collection
    {
        private readonly IRepository<Employee> repository;

        public Collection(string connectionString)
        {
            repository = new EmployeeRepository(connectionString);
        }

        public IEnumerable<Employee> GetEmployees() => repository.GetAll();

        public async Task<IEnumerable<Employee>> GetEmployeesAsync() => await repository.GetAllAsync();

        public void WriteError(Exception exception) => repository.LogError(exception);

        public async Task WriteErrorAsync(Exception exception) => await repository.LogErrorAsync(exception);
    }
}
