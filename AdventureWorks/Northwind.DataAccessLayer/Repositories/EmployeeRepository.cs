using System;
using System.Threading.Tasks;
using Northwind.Models;
using Northwind.DataAccessLayer.Interfaces;
using Northwind.DataAccessLayer.DBHandling;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Northwind.DataAccessLayer.Repositories
{
    public class EmployeeRepository : IRepository<Employee>
    {
        private readonly SqlConnection sqlConnection;

        public EmployeeRepository(string connectionString)
        {
            sqlConnection = new SqlConnection(connectionString);
        }

        private void OpenConnection()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Error(ex.Message, nameof(Northwind.DataAccessLayer.Repositories.EmployeeRepository.OpenConnection), DateTime.Now);
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
                throw new Error(ex.Message, nameof(Northwind.DataAccessLayer.Repositories.EmployeeRepository.CloseConnection), DateTime.Now);
            }
        }

        public IEnumerable<Employee> GetAll()
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
                throw new Error(ex.Message, nameof(Northwind.DataAccessLayer.Repositories.EmployeeRepository), DateTime.Now);
            }
            finally
            {
                CloseConnection();
            }
            return employees;
         }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await Task.Run(() => GetAll());
        }

        public void LogError(Exception exception)
        {
            OpenConnection();
            SqlTransaction transaction = sqlConnection.BeginTransaction();
            try
            {
                SqlCommand command = sqlConnection.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.CommandText = "LogError";
                DBWriter<Error> writer = new DBWriter<Error>(command, (Error)exception);
                writer.WriteDB();
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new Error(ex.Message, nameof(Northwind.DataAccessLayer.Repositories.EmployeeRepository), DateTime.Now);
            }
            finally
            {
                CloseConnection();
            }
        }

        public async Task LogErrorAsync(Exception exception)
        {
            await Task.Run(() => LogError(exception));
        }
    }
}