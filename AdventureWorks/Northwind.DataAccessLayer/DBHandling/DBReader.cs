using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Northwind.Models;

namespace Northwind.DataAccessLayer.DBHandling
{
    class DBReader
    {
        private readonly SqlCommand sqlCommand;

        public DBReader(SqlCommand command)
        {
            sqlCommand = command;
        }

        public IEnumerable<T> ReadDB<T>() where T : new()
        {
            using (var reader = sqlCommand.ExecuteReader())
            {
                if (!reader.HasRows)
                    return Enumerable.Empty<T>();
                List<T> entities = new List<T>();
                var properties = typeof(T).GetProperties();
                while (reader.Read())
                {
                    T obj = new T();
                    foreach (var property in properties)
                    {
                        property.SetValue(obj, reader[property.Name] is DBNull ? null : reader[property.Name]);
                    }
                    entities.Add(obj);
                }
                return entities;
            }
        }
    }
}
