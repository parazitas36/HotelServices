using HotelServices.Controllers;
using HotelServices.Data;
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
    public class DeleteAccModel : PageModel
    {
        private readonly ILogger<DeleteAccModel> _logger;
        private readonly IConfiguration _config;
        DBController db;
        SqlConnection dbc;
        public int unpaidCount = 0;
        public Client client;

        public DeleteAccModel(ILogger<DeleteAccModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            db = new DBController(config);
            
        }

        public void OnPost()
        {
            Client c = HttpContext.Session.GetObjectFromJson<Client>("ClientData");
            dbc = db.ConnectToDB();
            // Panaikinti apmokejimus
            string query = @"
            DELETE FROM Apmokejimas
            WHERE (SELECT saskaitos_nr FROM Saskaita WHERE fk_Klientasid_Naudotojas=@clientid) = fk_Saskaitasaskaitos_nr
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();

            // Panaikinti papildomas paslaugas
            query = @"
            DELETE FROM Papildoma_paslauga
            WHERE fk_klientas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();

            // Panaikinti saskaitas
            query = @"
            DELETE FROM Saskaita
            WHERE fk_Klientasid_Naudotojas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();

            // Panaikinti rezervacija
            query = @"
            DELETE FROM Rezervacija
            WHERE fk_Klientasid_Naudotojas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();

            // Panaikinti kliento duomenis
            query = @"
            DELETE FROM Klientas
            WHERE id_Naudotojas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();

            // Panaikinti paskyra
            query = @"
            DELETE FROM Naudotojas
            WHERE id_Naudotojas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();
            HttpContext.Session.Clear();
            Response.Redirect("/index");
        }

        public void OnGet()
        {
            Client c = HttpContext.Session.GetObjectFromJson<Client>("ClientData");
            client = c;
            dbc = db.ConnectToDB();

            // Gauti kliento saskaitu kieki
            string query = @"
            SELECT COUNT(saskaitos_nr)
            FROM Saskaita
            WHERE fk_Klientasid_Naudotojas = @clientid AND busena = 1
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", client.ID);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            unpaidCount = (int)reader[0];
            reader.Close();
            dbc.Close();
        }
    }
}
