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
    public class AtliktasModel : PageModel
    {
        private readonly ILogger<ReservationsModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Work> work;

        public AtliktasModel(ILogger<AtliktasModel> logger, IConfiguration config)
        {
            _config = config;
            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void OnGet(int id)
        {
            System.Diagnostics.Debug.WriteLine(id);
            DateTime now = DateTime.Now;
            work = new List<Work>();
            string query = @"
             INSERT INTO Darbuotojo_atliktas_darbas(atlikimo_data, fk_Darbuotojasid_Naudotojas, fk_Darbasid_Darbas)
            VALUES(@date, 2, @id);
            ";

            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@date", now);
            SqlDataReader reader = cmd.ExecuteReader();

           

        }
        public ActionResult OnPostWay2(int id)
        {
            return Redirect("/Workers/WorkList");
        }
           
        
    }
}
