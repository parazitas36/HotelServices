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
namespace HotelServices.Pages.Workers
{
    public class AtvykimasModel : PageModel
    {
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public AtvykimasModel(ILogger<AtliktasModel> logger, IConfiguration config)
        {
            _config = config;
            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void OnGet(int id)
        {
            id = 2;
            var Date = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;
            string query = @"
            SELECT t1.data, t1.fk_Darbuotojasid_Naudotojas
            FROM Darbo_valandu_fiksavimas t1  
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            int c = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    if ((DateTime)Date == (DateTime)results[0] && id == (int)results[1])
                    {
                        c++;

                    }

                }
                reader.Close();
            }
            dbc.Close();
            if (c == 0)
                sss();
        }
 public void sss()
    {
            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();
            int   id = 2;
            var Date = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;
            string query = @"
             INSERT INTO Darbo_valandu_fiksavimas(data,atvykimo_laikas ,fk_Darbuotojasid_Naudotojas )
             VALUES(@date, @time, @id);
            ";

            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@date", Date);
            cmd.Parameters.AddWithValue("@time", time);
            SqlDataReader reader = cmd.ExecuteReader();
            dbc.Close();

        }
        public ActionResult OnPostWay2(int id)
        {
            return Redirect("/Workers/WorkerProfile");
        }

        }
   
    }

