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
    public class WorkListModel : PageModel
    {
        private readonly ILogger<WorkListModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Work> work;

        public WorkListModel(ILogger<WorkListModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void GetWork()
        {
            work = new List<Work>();
            string query = @"
            SELECT t1.id_Darbas, t1.pavadinimas
            FROM Darbas t1
             LEFT JOIN Darbuotojo_atliktas_darbas b
             on t1.id_Darbas = b.fk_Darbasid_Darbas
            WHERE   b.fk_Darbasid_Darbas IS NULL    
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Work reservation = new Work
                    (
                        (int)results[0],
                        (string)results[1]
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
