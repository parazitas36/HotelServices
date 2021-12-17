using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HotelServices.Controllers
{
    public class DBController : ControllerBase
    {
        private readonly IConfiguration _config;
        public DBController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection ConnectToDB()
        {
            SqlConnection conn = new SqlConnection(_config.GetConnectionString("Default"));
            conn.Open();
            return conn;
        }
    }
}
