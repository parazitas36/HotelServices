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
    public class ReservationsModel : PageModel
    {
        private readonly ILogger<ReservationsModel> _logger;
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Reservation> reservations;

        public ReservationsModel(ILogger<ReservationsModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void GetReservations()
        {
            reservations = new List<Reservation>();
            string query = @"
            SELECT r.id_Rezervacija, r.pradzia, r.pabaiga, (SELECT rb.name FROM Rezervacijos_busena rb WHERE rb.id_Rezervacijos_busena = r.rezervacijos_busena) as busena, r.fk_Kambarysnr
            FROM Rezervacija r
            WHERE r.fk_Klientasid_Naudotojas = @clientid
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", 1);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Reservation reservation = new Reservation
                    (
                        (int)results[0],
                        (DateTime)results[1],
                        (DateTime)results[2],
                        (string)results[3],
                        (int)results[4]
                    );
                    reservations.Add(reservation);
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
