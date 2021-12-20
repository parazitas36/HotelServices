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

namespace HotelServices.Pages.Workers
{
    public class RoomListModel : PageModel
    {
        private readonly ILogger<RoomListModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Kambarys> work;

        public RoomListModel(ILogger<RoomListModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void GetWork()
        {
            work = new List<Kambarys>();
            string query = @"
            SELECT t1.nr, t1.statusas
            FROM Kambarys t1    
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Kambarys reservation = new Kambarys
                    (
                        (int)results[0],
                        (int)results[1]
                    );
                    work.Add(reservation);
                }
                reader.Close();
            }
            dbc.Close();
        }

        public void OnGet()
        {

        }
    }
}
