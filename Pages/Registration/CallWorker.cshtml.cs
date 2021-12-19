using HotelServices.Controllers;
using HotelServices.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class CallWorkerModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public Reservation rezervacija;
        public int id { get; set; }

        public int resid;
        public int roomid = 0;

        public CallWorkerModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet()
        {
        }
        public void OnPos()
        {
            string query = @"";
        }
        public void GetReservation() 
        {
            
        }
    }
}
