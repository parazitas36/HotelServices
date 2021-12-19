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
    public class PayBillCashModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public BillClients bill;

        public int sid = 0;
        public decimal sum = 0;
        public string err = "";
        public int resid = 0;

        public PayBillCashModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void OnGet(int id, int id2, string id3)
        {
            sid = id;
            sum = id2;
            err = id3;
            Console.WriteLine(err);
            GetBill();
            resid = bill.ReservationId;

        }
        public async void OnPostButton(int id, decimal id2, int id3, int id4)
        {
            Console.WriteLine(id2);
            sum = decimal.Parse(Request.Form["suma"].ToString());

            if (sum >= id2)
            {
                //atnaujina saskaitos busena i apmoketa
                string query = @"Update Saskaita SET busena = 2 WHERE Saskaita.saskaitos_nr = @id";
                SqlCommand cmd = new SqlCommand(query, dbc);
                Console.WriteLine(id);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();

                //sukuria apmokejima
                string query1 = @"insert into Apmokejimas (data, suma, apmokejimo_paskirtis, apmokejimo_budas, 
                fk_Saskaitasaskaitos_nr) values (@date, 150, 'Uz rezervacija', 2, @sid)";
                SqlCommand cmd1 = new SqlCommand(query1, dbc);
                cmd1.Parameters.AddWithValue("@sid", id);
                cmd1.Parameters.AddWithValue("@date", DateTime.Now.Date);
                cmd1.ExecuteNonQuery();


                //Gauna rezervacijos kambario nr
                string query3 = @"select Rezervacija.fk_Kambarysnr from Rezervacija where Rezervacija.id_Rezervacija = @resid";
                SqlCommand cmd3 = new SqlCommand(query3, dbc);
                cmd3.Parameters.AddWithValue("@resid", id4);

                SqlDataReader reader = cmd3.ExecuteReader();

                int roomid = 0;

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        IDataRecord results = (IDataRecord)reader;
                        roomid = (int)results[0];
                    }
                    reader.Close();
                }



                //keicia kambario statusa i tvarkomas
                string query2 = @"Update Kambarys Set Kambarys.statusas = 2 where Kambarys.nr = @id2";
                SqlCommand cmd2 = new SqlCommand(query2, dbc);
                cmd2.Parameters.AddWithValue("@id2", roomid);
                cmd2.ExecuteNonQuery();

                Response.Redirect("/Registration/RegBillsindex?ID=" + 1.ToString());
            }
            else
            {
                Response.Redirect("/Registration/PayBillCash?ID=" + id.ToString() + "&ID2=" + id2.ToString() + "&ID3=" + 1.ToString());
            }

        }
        public void GetBill()
        {
            string query = @"select Saskaita.saskaitos_nr, Saskaita.sudarymo_data, Saskaita.suma, 
            Saskaitos_busenos.name, Saskaita.fk_Registraturos_darbuotojasid_Naudotojas, 
            Saskaita.fk_Rezervacijaid_Rezervacija, Klientas.vardas, Klientas.pavarde 
            from Saskaita, Saskaitos_busenos, Klientas where Saskaita.busena = Saskaitos_busenos.id_Saskaitos_busenos 
            and Saskaita.fk_Klientasid_Naudotojas = Klientas.id_Naudotojas and Saskaita.saskaitos_nr = @sid";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@sid", sid);
            SqlDataReader reader = cmd.ExecuteReader();

            bill = new BillClients();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    BillClients bill1 = new BillClients
                    (
                        (int)results[0],
                        (DateTime)results[1],
                        (decimal)results[2],
                        (string)results[3],
                        (int)results[4],
                        (int)results[5],
                        (string)results[6],
                        (string)results[7]
                    );
                    bill = bill1;
                }
                reader.Close();
            }
            dbc.Close();
        }
    }
}
