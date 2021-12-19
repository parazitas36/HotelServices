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
    public class RegCreateBillModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public Reservation rezervacija;
        public int resid = 0;
        public int cid = 0;

        public RegCreateBillModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void OnGet(int id)
        {
            resid = id;
            GetReservation();
        }
        public async void OnPostButton(int id, int id2)
        {
            //sukuria saskaita
            string query = @"insert into Saskaita (sudarymo_data, suma, busena, fk_Registraturos_darbuotojasid_Naudotojas, 
            fk_Rezervacijaid_Rezervacija, fk_Klientasid_Naudotojas) 
            values (@date, 150, 1, @wid, @resid, @cid)";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@cid", id2);
            cmd.Parameters.AddWithValue("@resid", id);
            RDarbuotojas rd = HttpContext.Session.GetObjectFromJson<RDarbuotojas>("rworker");
            int wid = rd.ID;
            cmd.Parameters.AddWithValue("@wid", wid);
            cmd.Parameters.AddWithValue("@date", DateTime.Now.Date);

            cmd.ExecuteNonQuery();

            Response.Redirect("/Registration/RegRezervationsindex");
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
                    cid = (int)results[5];
                    Console.WriteLine(cid);
                    rezervacija = rezervacija1;
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
