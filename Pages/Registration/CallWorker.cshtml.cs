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
        public int id { get; set; }


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
        public void OnPost()
        {
            string query = @"insert into Iskvietimas (pranesimo_tekstas, priimtas, fk_Registraturos_darbuotojasid_Naudotojas) 
            values ('Laukiama registraturoje', 0, @id)";
            SqlCommand cmd = new SqlCommand(query, dbc);
            RDarbuotojas rd = HttpContext.Session.GetObjectFromJson<RDarbuotojas>("rworker");
            int wid = rd.ID;
            cmd.Parameters.AddWithValue("@id", wid);
            cmd.ExecuteNonQuery();

            dbc.Close();

            Response.Redirect("/Registration/index?ID=" + 1.ToString());
        }
        public void GetReservation() 
        {
            
        }
    }
}
