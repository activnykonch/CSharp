using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccessLayer.DBHandling
{
    class DBWriter<T> where T : class
    {
        private readonly SqlCommand sqlCommand;

        private readonly T Data;

        public DBWriter(SqlCommand command, T data)
        {
            sqlCommand = command;
            Data = data;
        }

        public void WriteDB()
        {
            var propeties = this.Data.GetType().GetProperties();
            foreach(var property in propeties)
            {
                if (property.DeclaringType == Data.GetType())
                {
                    SqlParameter parameter = new SqlParameter
                    {
                        ParameterName = property.Name,
                        Value = property.GetValue(Data)
                    };
                    sqlCommand.Parameters.Add(parameter);
                }
            }
            sqlCommand.ExecuteNonQuery();
        }
    }
}
