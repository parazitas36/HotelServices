using HotelServices.Controllers;
using HotelServices.Data;
using HotelServices.Models;
using HotelServices.Services;
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
    public class ReserveModel : PageModel
    {
        private readonly ILogger<ReserveModel> _logger;
        private readonly IConfiguration _config;
        private readonly IMailService _mailService;
        public string ERROR = "";

        DBController db;
        SqlConnection dbc;

        public ReserveModel(ILogger<ReserveModel> logger, IConfiguration config, IMailService mailService)
        {
            _logger = logger;
            _config = config;
            _mailService = mailService;
            db = new DBController(config);
        }

        public void OnPost(DateTime start, DateTime end)
        {
            ERROR = "";
            if (start > end) { ERROR = "Klaidingai pasirinkta data."; return; }

            // Atrinkti uzimtus kambarius pagal data
            dbc = db.ConnectToDB();
            string query = @"
            SELECT  *
            FROM Kambarys
            LEFT JOIN Rezervacija r
            ON Kambarys.nr = r.fk_Kambarysnr
            WHERE ((SELECT COUNT(fk_Kambarysnr) FROM Rezervacija rr WHERE rr.fk_Kambarysnr = r.fk_Kambarysnr AND (rr.pradzia <= @start AND rr.pabaiga >= @end)) = 0) AND (r.pradzia IS NULL OR r.pradzia > @end) OR (r.pabaiga IS NULL OR r.pabaiga < @start)
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows) { ERROR = "Nera laisvu kambariu siuo laiko intervalu, bandykite kita laika."; return; }
            reader.Read();
            IDataRecord firstRoom = (IDataRecord)reader;
            int firstRoomID = (int)firstRoom[0];
            reader.Close();

            Client client = HttpContext.Session.GetObjectFromJson<Client>("ClientData");
            // Paimti kliento email
            query = @"
            SELECT el_pastas
            FROM Naudotojas
            WHERE id_Naudotojas = @clientid
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", client.ID);
            reader = cmd.ExecuteReader();
            reader.Read();
            string email = (string)reader[0];
            reader.Close();

            // Uzrezervuoti
            query = @"
            INSERT INTO Rezervacija
            (pradzia, pabaiga, fk_Klientasid_Naudotojas, rezervacijos_busena, fk_Kambarysnr)
            VALUES(@start, @end, @clientid, 1, @roomnr)
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@start", start);
            cmd.Parameters.AddWithValue("@end", end);
            cmd.Parameters.AddWithValue("@clientid", client.ID);
            cmd.Parameters.AddWithValue("@roomnr", firstRoomID);


            if (cmd.ExecuteNonQuery() != 0)
            {
                // Issiusti el.laiska su rezervacijos duomenim
                MailController mc = new MailController(_mailService);
                MailRequest mr = new MailRequest
                {
                    Subject = "Jūsų rezervacija sėkminga",
                    ToEmail = email,
                    Body = String.Format($"<h3>Siunčiame Jūsų rezervacijos duomenis</h3><br/><b>Kambario numeris:</b>{firstRoomID}<br/><b>Data:</b>Nuo {start.ToString("yyyy/MM/dd")} iki {end.ToString("yyyy/MM/dd")}")
                };

                mc.SendMail(mr);
            }
            else
            {
                ERROR = "Nepavyko uzregistruoti";
                return;
            }

            Response.Redirect("/Clients/Reservations");
        }

        public void OnGet()
        {
           
        }
    }
}
