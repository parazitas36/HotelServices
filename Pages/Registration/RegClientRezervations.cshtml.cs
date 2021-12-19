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
    public class RegClientRezervationsModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public IList<Reservation> rezervacijos;
        int cid = 0;

        public RegClientRezervationsModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet(int id)
        {
            cid = id;
        }
        public void GetRezervations()
        {
            string query = @"Select Rezervacija.id_Rezervacija, Rezervacija.pradzia, Rezervacija.pabaiga, Rezervacijos_busena.name, 
            Rezervacija.fk_Kambarysnr from Rezervacija, Rezervacijos_busena 
            Where Rezervacija.rezervacijos_busena = rezervacijos_busena.id_Rezervacijos_busena 
            and Rezervacija.fk_Klientasid_Naudotojas = @id";

            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", cid);
            SqlDataReader reader = cmd.ExecuteReader();
            rezervacijos = new List<Reservation>();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Reservation rezervacija = new Reservation
                    (
                        (int)results[0],
                        (DateTime)results[1],
                        (DateTime)results[2],
                        (string)results[3],
                        (int)results[4]
                    );
                    rezervacijos.Add(rezervacija);
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
