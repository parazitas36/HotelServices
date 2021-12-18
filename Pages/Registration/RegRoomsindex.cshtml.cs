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
    public class RegRoomsindexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public List<Kambarys> kambariai;
        public RegRoomsindexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;

            _config = config;
            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();

            string query = @"Select Kambarys.nr as Nr, Kambario_statusai.name as statusas from Kambarys, Kambario_statusai 
where Kambarys.statusas = Kambario_statusai.id_Kambario_statusai";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read()) 
            {
                kambariai = new List<Kambarys>();
                
            }

        }

        public void OnGet()
        {
           

        }
    }
}
