using System;
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

        public void WriteError(Exception ex) => repository.LogError(ex);
    }
}
