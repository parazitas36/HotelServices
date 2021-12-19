﻿using HotelServices.Controllers;
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
    public class AprroveReservationModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public Reservation rezervacija;
        public int id { get; set; }

        public int resid;
        public int roomid = 0;

        public AprroveReservationModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet(int id, int id2)
        {
            resid = id;
            roomid = id2;
            Console.WriteLine(id);
            GetReservation();
        }
        public async void OnPostButton(int id, int id2)
        {
            string query = @"UPDATE Rezervacija Set rezervacijos_busena = 3 Where Rezervacija.id_Rezervacija = @id";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id2);
            cmd.ExecuteNonQuery();

            string query2 = @"Update Kambarys Set Kambarys.statusas = 3 where Kambarys.nr = @id2";
            SqlCommand cmd2 = new SqlCommand(query2, dbc);
            cmd2.Parameters.AddWithValue("@id2", id);
            cmd2.ExecuteNonQuery();

            Response.Redirect("/Registration/RegRezervationsindex?ID=" + 1.ToString());
        }
        public void GetReservation() 
        {
            string query = @"Select Rezervacija.id_Rezervacija, Rezervacija.pradzia, Rezervacija.pabaiga, 
            Rezervacijos_busena.name, Rezervacija.fk_Kambarysnr, Rezervacija.fk_Klientasid_Naudotojas 
            from Rezervacija, Rezervacijos_busena Where Rezervacija.rezervacijos_busena = rezervacijos_busena.id_Rezervacijos_busena 
            and Rezervacija.id_Rezervacija = @resid";


            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@resid", resid);
            SqlDataReader reader = cmd.ExecuteReader();
          
            rezervacija = new Reservation();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Reservation rezervacija1 = new Reservation
                    (
                        (int)results[0],
                        (DateTime)results[1],
                        (DateTime)results[2],
                        (string)results[3],
                        (int)results[4]
                    );
                    rezervacija = rezervacija1;
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
