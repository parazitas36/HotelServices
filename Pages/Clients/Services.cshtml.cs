using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelServices.Controllers;
using HotelServices.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HotelServices.Pages.Clients
{
    public class ServicesModel : PageModel
    {
        private readonly ILogger<ServicesModel> _logger;
        private readonly IConfiguration _config;
        DBController db;
        public string ERROR = "";
        public int? BillID = null;
        public IList<Service> services;
        public IList<SelectListItem> servicesDDlist;


        public ServicesModel(ILogger<ServicesModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            db = new DBController(config);
        }

        public void OnPost(string service)
        {
            OnGet();
            Client c = HttpContext.Session.GetObjectFromJson<Client>("ClientData");
            SqlConnection dbc = db.ConnectToDB();
            string query = @"
            INSERT INTO Papildoma_paslauga
            (paslaugos_pavadinimas, kaina, paslaugos_busena, fk_Saskaitasaskaitos_nr, fk_Darbuotojasid_Naudotojas, fk_klientas)
            VALUES(@name, @price, 1, @billid, NULL, @clientid)
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            Service ser = services[Convert.ToInt32(service)];
            cmd.Parameters.AddWithValue("@name", ser.Name);
            cmd.Parameters.AddWithValue("@price", (double)ser.Price);
            cmd.Parameters.AddWithValue("@billid", BillID);
            cmd.Parameters.AddWithValue("@clientid", c.ID);
            cmd.ExecuteNonQuery();
            Response.Redirect("/Clients/Success");
        }

        public void FillLists()
        {
            // Uzpildyt paslaugu sarasa
            services = new List<Service>();
            services.Add(new Service
            {
                ID = "0",
                Name = "Dienos pietūs",
                Price = 5.00m
            }
            );
            services.Add(new Service
            {
                ID = "1",
                Name = "Kambarių tarnyba",
                Price = 10.00m
            }
            );

            servicesDDlist = new List<SelectListItem>();
            foreach (var item in services)
            {
                servicesDDlist.Add(
                    new SelectListItem
                    {
                        Text = "Paslauga: " + item.Name + " Kaina:" + item.Price.ToString() + "e",
                        Value = item.ID
                    }
                );
            }
        }

        public void OnGet()
        {
            Client client = HttpContext.Session.GetObjectFromJson<Client> ("ClientData");
            ERROR = "";

            // Patikrinti ar yra atidaryta saskaita
            SqlConnection dbc = db.ConnectToDB();
            string query = @"
            SELECT saskaitos_nr
            FROM Saskaita
            WHERE fk_Klientasid_Naudotojas = @clientid
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@clientid", client.ID);
            SqlDataReader reader = cmd.ExecuteReader();
            if(!reader.HasRows) { ERROR = "Šiuo metu negalite užsisakyti papildomų paslaugų, kadangi nėra atidarytų sąskaitų!"; return; }
            reader.Read();
            BillID = (int)reader[0];
            dbc.Close();
            FillLists();
        }
    }
}
