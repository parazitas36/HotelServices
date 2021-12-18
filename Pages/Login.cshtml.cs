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
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc; // Connection to database

        public LoginModel(IConfiguration config, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _config = config;
            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost(string username, string password)
        {
            Console.WriteLine(username);
            Console.WriteLine(password);

            // Paima id ir role, jei toks user yra
            string query = @"
            SELECT u.id_Naudotojas as id, (SELECT r.role FROM roles r WHERE Id = u.fk_role) as role 
            FROM Naudotojas u 
            WHERE u.prisijungimo_vardas = @username AND u.slaptazodis = @psw; 
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@psw", password);
            SqlDataReader reader = cmd.ExecuteReader();

            // Grazina error, jei tokio nerado
            if (!reader.HasRows) { Console.WriteLine("Error login..."); return Redirect("/index");}

            reader.Read();
            IDataRecord results = (IDataRecord)reader;
            int? id = (int?)results[0];
            string? role = (string)results[1];
            reader.Close();

            // Jei prisijunge klientas
            if(role == "client")
            {
                query = @"
                SELECT *
                FROM Klientas
                WHERE id_Naudotojas = @id;
                ";
                cmd = new SqlCommand(query, dbc);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                IDataRecord clientData = (IDataRecord)reader;

                Console.WriteLine("Client logged in.");
                Client client = new Client
                (
                    (int)id,
                    role,
                    (string)clientData[0],
                    (string)clientData[1],
                    (string)clientData[2],
                    (DateTime)clientData[3]
                );
                Console.WriteLine(client.Name);
                reader.Close();
                Response.Cookies.Append("role", "client");
                return Redirect("/clients/index");
            }
            //jei prisijunge registraturos darbuotojas
            if (role == "rworker")
            {
                query = @"
                SELECT *
                FROM Registraturos_darbuotojas
                WHERE id_Naudotojas = @id;
                ";
                cmd = new SqlCommand(query, dbc);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();
                reader.Read();
                IDataRecord clientData = (IDataRecord)reader;

                Console.WriteLine("Client logged in.");
                RDarbuotojas client = new RDarbuotojas
                (
                    (int)id,
                    role,
                    (string)clientData[0],
                    (string)clientData[1],
                    (DateTime)clientData[2]
                );
                Console.WriteLine(client);
                reader.Close();

                //sukuria cookie uz "role" kablelio galima rasyti ir nehardcodintas reiksmes pvz jwt tokena

                Response.Cookies.Append("role", "rworker");
                return Redirect("/registration/index");
                return Redirect("/registration/index");
            }
            dbc.Close();
            return Redirect("/index");
        }
    }
}
