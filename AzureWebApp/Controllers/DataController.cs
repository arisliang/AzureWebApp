using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureWebCommon;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AzureWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly DataRepository dataRepo;
        private readonly IConfiguration config;

        public DataController(DataRepository repo, IConfiguration config)
        {
            this.config = config;

            this.dataRepo = repo;
            string sa_account = "sa_demowebapptest";
            string password = Environment.GetEnvironmentVariable(sa_account);
            string server = "demo-webapptest1.database.windows.net";

            this.dataRepo.ConnectionString = config["ConnectionString"].Replace("{server}", server)
                .Replace("{sa_account}", sa_account)
                .Replace("{password}", password);
        }

        [HttpGet]
        [Route(nameof(GetCustomers))]
        public IList<Customer> GetCustomers()
        {
            var customers = this.dataRepo.GetCustomers();

            return customers;
        }
    }
}