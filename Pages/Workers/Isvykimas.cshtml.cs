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
    public class IsvykimasModel : PageModel
    {
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IsvykimasModel(ILogger<IsvykimasModel> logger, IConfiguration config)
        {
            _config = config;
            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void OnGet()
        {
 //INSERT INTO Darbo_valandu_fiksavimas(isvykimo_laikas )
 //            VALUES(@time)
 //            WHERE data=@date AND  fk_Darbuotojasid_Naudotojas=@id
            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();
            int id = 2;
            var Date = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;
            string query = @"UPDATE Darbo_valandu_fiksavimas SET isvykimo_laikas = @time  WHERE fk_Darbuotojasid_Naudotojas = @id AND data=@date
                                       ";

            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.Parameters.AddWithValue("@time", time);
            SqlDataReader reader = cmd.ExecuteReader();
            dbc.Close();

        }
        public ActionResult OnPostWay2(int id)
        {
            return Redirect("/Workers/WorkerProfile");
        }

    }
}
