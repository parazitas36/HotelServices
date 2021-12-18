using HotelServices.Controllers;
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
    public class RegisterModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public string ERROR = "";

        public RegisterModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet()
        {

        }

        public void OnPost(string username, string email, string password, string rpassword, 
            string name, string surname, string phone, DateTime birth)
        {
            ERROR = "";
            // Grazina klaida, jei slaptazodziai nesutampa
            if(password != rpassword)
            {
                Console.WriteLine("Slaptazodziai nesutampa!");
                return;
            }

            // Patikrina ar toks vartotojas neegzsituoja 
            string query = @"
            SELECT id_Naudotojas
            FROM Naudotojas
            WHERE prisijungimo_vardas = @username OR el_pastas = @email
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();

            // Grazina klaida, jei toks egzistuoja
            if (reader.HasRows) { ERROR  = "Toks prisijungimo vardas arba el.paštas jau yra naudojamas."; return; }
            reader.Close();

            // Sukuria paskyra ir grazina paskyros id
            query = @"
            INSERT INTO Naudotojas
            (prisijungimo_vardas, slaptazodis, el_pastas, fk_role)
            OUTPUT Inserted.id_Naudotojas
            VALUES(@uname, @psw, @email, 2);
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@psw", password);
            cmd.Parameters.AddWithValue("@email", email);
            reader = cmd.ExecuteReader();
            reader.Read();
            int insertedID = (int)reader[0];
            reader.Close();

            // Iterpia kliento duomenis
            query = @"
            INSERT INTO Klientas
            (vardas, pavarde, tel_nr, gimimo_data, id_Naudotojas)
            VALUES(@name, @surname, @phone, @date, @id)
            ";
            cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@phone", phone);
            cmd.Parameters.AddWithValue("@date", birth);
            cmd.Parameters.AddWithValue("@id", insertedID);
            cmd.ExecuteNonQuery();
            dbc.Close();
        }
    }
}
