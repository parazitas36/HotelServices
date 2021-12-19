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
    public class RegClientsindexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public IList<Klientas> klientai;

        public RegClientsindexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet()
        {

        }
        public void GetClients()
        {
            string query = @"Select * from Klientas";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            klientai = new List<Klientas>();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Klientas klientas = new Klientas
                    (
                        (string)results[0],
                        (string)results[1],
                        (string)results[2],
                        (DateTime)results[3],
                        (int)results[4]
                    );
                    klientai.Add(klientas);
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
