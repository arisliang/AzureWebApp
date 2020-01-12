using AzureWebCommon;
using System;
using Xunit;

namespace AzureWebTests
{
    public class AzureSQLTests
    {
        [Fact]
        public void Test_GetSQLData()
        {
            string sa_account = "sa_demowebapptest";
            string password = Environment.GetEnvironmentVariable(sa_account);
            string server = "demo-webapptest1.database.windows.net";

            string connStr = $"Server=tcp:{server},1433;Initial Catalog=demowebapp;Persist Security Info=False;User ID={sa_account};Password={password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            var repo = new DataRepository
            {
                ConnectionString = connStr
            };

            var customers = repo.GetCustomers();

            Assert.NotNull(customers);
            Assert.NotEmpty(customers);
        }
    }
}
