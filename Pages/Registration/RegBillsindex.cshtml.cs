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
    public class RegBillsindexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public IList<Bill> Bills;

        public int success = 0;

        public RegBillsindexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet(int id)
        {
            success = id;
        }
        public void GetBills()
        {
            string query = @"select Saskaita.saskaitos_nr, Saskaita.sudarymo_data, Saskaita.suma, 
            Saskaitos_busenos.name, Saskaita.fk_Registraturos_darbuotojasid_Naudotojas, 
            Saskaita.fk_Rezervacijaid_Rezervacija, Saskaita.fk_Klientasid_Naudotojas 
            from Saskaita, Saskaitos_busenos where Saskaita.busena = Saskaitos_busenos.id_Saskaitos_busenos";

            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();

            Bills = new List<Bill>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Bill bill = new Bill
                    (
                        (int)results[0],
                        (DateTime)results[1],
                        (Decimal)results[2],
                        (string)results[3],
                        (int)results[4],
                        (int)results[5],
                        (int)results[6]
                    );
                    Bills.Add(bill);
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
