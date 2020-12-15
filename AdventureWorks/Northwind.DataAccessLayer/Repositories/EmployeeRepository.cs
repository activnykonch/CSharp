using System;
using Northwind.Models;
using Northwind.DataAccessLayer.Interfaces;
using Northwind.DataAccessLayer.DBHandling;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Northwind.DataAccessLayer.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        public string ConnectionString { get; private set; }
        private readonly SqlConnection sqlConnection;

        public EmployeeRepository(string connectionString)
        {
            ConnectionString = connectionString;
            sqlConnection = new SqlConnection(ConnectionString);
        }

        private void OpenConnection()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CloseConnection()
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        IEnumerable<Employee> IRepository<Employee>.GetAll()
        {
            OpenConnection();

            IEnumerable<Employee> employees;

            SqlTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                SqlCommand command = sqlConnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "GetEmployees";
                var reader = new DBReader(command);
                employees = reader.ReadDB<Employee>();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return employees;
         }
    }
}