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
    public class RegisterworkerModel : PageModel
    {
        private readonly ILogger<RegisterworkerModel> _logger;
        private readonly IConfiguration _config;
        public string ERROR = "";

        DBController db;

        public RegisterworkerModel(ILogger<RegisterworkerModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            db = new DBController(config);
        }

        public void OnPost(string username, string email, string password, string name, string surname, DateTime birth)
        {
            SqlConnection dbc = db.ConnectToDB();
            ERROR = "";

            // Patikrinti ar tokio nera
            string checkQuery = @"
            SELECT id_Naudotojas
            FROM Naudotojas
            WHERE prisijungimo_vardas = @username OR el_pastas = @email
            ";
            SqlCommand checkCmd = new SqlCommand(checkQuery, dbc);
            checkCmd.Parameters.AddWithValue("@username", username);
            checkCmd.Parameters.AddWithValue("@email", email);
            if(checkCmd.ExecuteNonQuery() > 0) { ERROR = "Nepavyko užregistruoti, vartotojas su tokiu prisijungimo vardu arba el.paštu jau yra!"; return; }

            // Sukurti paskyra
            string query = @"
            INSERT INTO Naudotojas
            (prisijungimo_vardas, slaptazodis, el_pastas, fk_role)
            OUTPUT Inserted.id_Naudotojas
            VALUES(@username, @psw, @email, 4)
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@psw", password);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int accID = (int)reader[0];
            reader.Close();
            Console.WriteLine(accID);
            // Sukurti darbuotoja
            query = @"
            INSERT INTO Darbuotojas
            (vardas, pavarde, gimimo_data, id_Naudotojas)
            VALUES(@name, @surname, @birth, @accid)
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@birth", birth);
            cmd.Parameters.AddWithValue("@accid", accID);
            cmd.ExecuteNonQuery();
            dbc.Close();
            Response.Redirect("/Admin/Workerslist");
        }

        public void OnGet()
        {

        }
    }
}
