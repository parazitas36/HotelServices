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
    public class CancelReservationModel : PageModel
    {
        private readonly ILogger<CancelReservationModel> _logger;
        private readonly IConfiguration _config;
        public Reservation reservation = null;
        private SqlConnection dbc;
        DBController db;
        public string ERROR = "";
        public int resID = 0;

        public CancelReservationModel(ILogger<CancelReservationModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGetOnClick(int id)
        {
            dbc = db.ConnectToDB();
            string query = @"
            DELETE FROM Rezervacija
            WHERE id_Rezervacija = @id
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            Response.Redirect("/Clients/Reservations");
        }

        public void OnGet(int id)
        {
            ERROR = "";
            resID = id;
            Client c = HttpContext.Session.GetObjectFromJson<Client>("ClientData");

            // Paimti rezervacijos duomenis
            string query = @"
            SELECT r.id_Rezervacija, r.pradzia, r.pabaiga, (SELECT rb.name FROM Rezervacijos_busena rb WHERE rb.id_Rezervacijos_busena = r.rezervacijos_busena) as busena, r.fk_Kambarysnr
            FROM Rezervacija r
            WHERE r.fk_Klientasid_Naudotojas = @clientid AND r.id_Rezervacija = @id 
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();

            if(!reader.HasRows) { ERROR = "Tokios rezervacijos rasti nepavyko arba ji ne jūsų."; return; }
            reader.Read();

            IDataRecord results = (IDataRecord)reader;
            reservation = new Reservation
            (
                (int)results[0],
                (DateTime)results[1],
                (DateTime)results[2],
                (string)results[3],
                (int)results[4]
            );

            reader.Close();
            dbc.Close();
        }
    }
}
