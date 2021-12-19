﻿using HotelServices.Controllers;
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
    public class RegRoomsindexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private SqlConnection dbc;
        public IList<Kambarys> kambariai;
        public int success = 0;
        public RegRoomsindexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            DBController db = new DBController(config);
            dbc = db.ConnectToDB();
        }

        public void ReadRooms()
        {

            string query = @"Select Kambarys.nr as Nr, Kambario_statusai.name as statusas from Kambarys, Kambario_statusai 
            where Kambarys.statusas = Kambario_statusai.id_Kambario_statusai";
            SqlCommand cmd = new SqlCommand(query, dbc);
            SqlDataReader reader = cmd.ExecuteReader();
            kambariai = new List<Kambarys>();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    IDataRecord results = (IDataRecord)reader;
                    Kambarys kambarys = new Kambarys
                    (
                        (int)results[0],
                        (string)results[1]
                    );
                    kambariai.Add(kambarys);
                }
                reader.Close();
            }
            dbc.Close();
        }

        public void OnGet(int id)
        {
            success = id;
        }
    }
}
