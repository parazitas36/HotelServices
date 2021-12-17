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
    public class LoginModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;


        public LoginModel(IConfiguration config, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _config = config;
        }

        public void OnGet()
        {

        }
        public void OnPost(string email, string password)
        {
            Console.WriteLine(email);
            Console.WriteLine(password);
            DBController dbc = new DBController(_config);
            SqlConnection conn = dbc.ConnectToDB();
            Console.WriteLine(conn.State);
            if (email.ToLower().Contains("klientas")) {
                Response.Redirect("/clients/index");
            }
            else if (email.ToLower().Contains("rdarbuotojas"))
            {
                Response.Redirect("/registration/index");
            }
            else if (email.ToLower().Contains("darbuotojas"))
            {
                Response.Redirect("/workers/index");
            }
            else if (email.ToLower().Contains("admin"))
            {
                Response.Redirect("/admin/index");
            }

        }
    }
}
