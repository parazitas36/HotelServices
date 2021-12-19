using HotelServices.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class DeleteworkerModel : PageModel
    {
        private readonly ILogger<DeleteworkerModel> _logger;
        private readonly IConfiguration _config;
        public int? deleteID = null;
        DBController db;

        public DeleteworkerModel(ILogger<DeleteworkerModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            db = new DBController(config);
        }

        public void OnGetOnClick(int id)
        {
            deleteID = id;
            SqlConnection dbc = db.ConnectToDB();

            // Pasalint darbo grafika
            string query = @"
            DELETE FROM Darbo_grafikas
            WHERE fk_Darbuotojasid_Naudotojas = @workerid
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            // Pasalint darbo valandu fiksavima
            query = @"
            DELETE FROM Darbo_valandu_fiksavimas
            WHERE fk_Darbuotojasid_Naudotojas = @workerid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            // Pasalint darbuotojo atlikta darba
            query = @"
            DELETE FROM Darbuotojo_atliktas_darbas
            WHERE fk_Darbuotojasid_Naudotojas = @workerid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            // Atnaujint papildomas paslaugas
            query = @"
            UPDATE Papildoma_paslauga
            SET fk_Darbuotojasid_Naudotojas = NULL
            WHERE fk_Darbuotojasid_Naudotojas = @workerid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            // Panaikinti darbuotojo asmeninius duomenis
            query = @"
            DELETE FROM Darbuotojas
            WHERE id_Naudotojas = @workerid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            // Panaikinti darbuotojo paskyra
            query = @"
            DELETE FROM Naudotojas
            WHERE id_Naudotojas = @workerid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@workerid", deleteID);
            cmd.ExecuteNonQuery();

            dbc.Close();
            Response.Redirect("/Admin/workerslist");
        }

        public void OnGet(int id)
        {
            deleteID = id;
        }
    }
}
