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
    public class WorkerProfileModel : PageModel
    {
        private readonly ILogger<WorkerProfileModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Darbuotojas> work;

        public WorkerProfileModel(ILogger<WorkerProfileModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void GetWorker()
        {
            work = new List<Darbuotojas>();
            string query = @"
            SELECT t1.vardas, t1.pavarde, t1.gimimo_data
            FROM Darbuotojas t1   
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Darbuotojas reservation = new Darbuotojas
                    (
                        (string)results[0],
                        (string)results[1],
                        (DateTime)results[2]
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
