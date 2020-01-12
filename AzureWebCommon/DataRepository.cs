using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace AzureWebCommon
{
    public abstract class BaseRepository
    {
        public string ConnectionString { get; set; }

        public SqlConnection CreateSqlConnection()
        {
            if (string.IsNullOrWhiteSpace(ConnectionString))
            {
                throw new ApplicationException("Please configure connection string");
            }

            return new SqlConnection(ConnectionString);
        }

        public SqlCommand CreateSqlCommand(string cmdText, SqlConnection connection)
        {
            return new SqlCommand(cmdText, connection);
        }
    }

    public class DataRepository : BaseRepository
    {
        public IList<Customer> GetCustomers()
        {
            var list = new List<Customer>();

            using (var connection = CreateSqlConnection())
            {
                connection.Open();

                using (var cmd = CreateSqlCommand("select * from dbo.fGetCustomers()", connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = PopulateCustomer(reader);

                            list.Add(customer);
                        }
                    }
                }
            }

            return list;
        }

        #region helpers

        private Customer PopulateCustomer(SqlDataReader reader)
        {
            return new Customer
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
        }

        #endregion
    }
}
