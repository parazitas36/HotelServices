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
    public class SummaryModel : PageModel
    {
        private readonly ILogger<SummaryModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Darbuotojas> work;
        public double total;
        public SummaryModel(ILogger<SummaryModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
        public List<Work> results { get; set; }
        public List<Ataskaita> results2 { get; set; }
        public void OnGet(string id)
        {

            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();
            results = new List<Work>();
            results2 = new List<Ataskaita>();
            List<int> darbas = new List<int>();
            string query = @"
            SELECT t2.pavadinimas , t1.atlikimo_data, t1.fk_Darbasid_Darbas
            FROM Darbuotojo_atliktas_darbas t1
            LEFT JOIN Darbas t2
                        ON t1.fk_Darbasid_Darbas=t2.id_Darbas 
                
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id",id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord result = (IDataRecord)reader;
                    Work reservation = new Work
                         (
                                (int)result[2],
                                 (string)result[0],
                                 (DateTime)result[1]

                                 );
                    results.Add(reservation);
                
                }
                reader.Close();
            }

        }
    }
}
