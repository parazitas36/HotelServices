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
    public class WorkerslistModel : PageModel
    {
        private readonly ILogger<WorkerslistModel> _logger;
        private readonly IConfiguration _config;
        DBController db;
        public IList<Worker> workers;

        public WorkerslistModel(ILogger<WorkerslistModel> logger, IConfiguration config)
        {
            _logger = logger;
            db = new DBController(config);
        }

        public void OnGet()
        {
            SqlConnection dbc = db.ConnectToDB();
            workers = new List<Worker>();

            // Paimti visu darbuotoju duomenis
            string query = @"
            SELECT *
            FROM Darbuotojas
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                IDataRecord results = (IDataRecord)reader;
                Worker worker = new Worker(
                    (string)results[0],
                    (string)results[1],
                    (DateTime)results[2],
                    (int)results[3]
                );
                workers.Add(worker);
            }
            reader.Close();
            dbc.Close();
        }
    }
}
