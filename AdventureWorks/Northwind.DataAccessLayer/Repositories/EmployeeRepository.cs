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
<<<<<<< HEAD
        public string ConnectionString { get; private set; }
=======
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
        private readonly SqlConnection sqlConnection;

        public EmployeeRepository(string connectionString)
        {
<<<<<<< HEAD
            ConnectionString = connectionString;
            sqlConnection = new SqlConnection(ConnectionString);
=======
            sqlConnection = new SqlConnection(connectionString);
>>>>>>> 11a14985404befefc2083c4d4468b2eb9064e369
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