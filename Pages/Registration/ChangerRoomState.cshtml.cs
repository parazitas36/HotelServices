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
    public class ChangerRoomStateModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public Kambarys kambarys;
        public int rid = 0;
        public IList<RoomState> statusai;
        public ChangerRoomStateModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }
        public void OnGet(int id)
        {
            rid = id;
            ReadRoom();
        }
        public void ReadRoom()
        {
            //kambario nuskaitymas
            string query = @"Select Kambarys.nr, Kambario_statusai.name from Kambarys, Kambario_statusai where Kambarys.nr = @id 
            and Kambarys.statusas = Kambario_statusai.id_Kambario_statusai";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", rid);
            SqlDataReader reader = cmd.ExecuteReader();
            kambarys = new Kambarys();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Kambarys kambarys1 = new Kambarys
                    (
                        (int)results[0],
                        (string)results[1]
                    );
                    kambarys = kambarys1;
                }
                reader.Close();
            }

            //statusu surinkimas
            string query1 = @"select * from Kambario_statusai";
            SqlCommand cmd1 = new SqlCommand(query1, dbc);
            SqlDataReader reader1 = cmd1.ExecuteReader();

            statusai = new List<RoomState>();

            if (reader1.HasRows)
            {
                while (reader1.Read())
                {
                    IDataRecord results = (IDataRecord)reader1;
                    RoomState state = new RoomState
                    (
                        (int)results[0],
                        (string)results[1]
                    );
                    statusai.Add(state);
                }
                reader.Close();
            }

            dbc.Close();
        }
        public async void OnPostButton(int id)
        {
            Console.WriteLine(Request.Form["states"]);
            int number = int.Parse(Request.Form["states"].ToString());
            string query2 = @"Update Kambarys Set Kambarys.statusas = @id2 where Kambarys.nr = @id1";
            SqlCommand cmd2 = new SqlCommand(query2, dbc);
            cmd2.Parameters.AddWithValue("@id1", id);
            cmd2.Parameters.AddWithValue("@id2", number);
            cmd2.ExecuteNonQuery();

            Response.Redirect("/Registration/RegRoomsindex?ID=" + 1.ToString());
        }
    }
}
