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
    public class WorkerProfileEditModel : PageModel
    {
        private readonly IConfiguration _config;
        public SqlConnection dbc;
        public IList<Darbuotojas> worker;
        private readonly ILogger<WorkerProfileEditModel> _logger;
        public int id2;
        public WorkerProfileEditModel(ILogger<WorkerProfileEditModel> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

        }
        [BindProperty]
        public Darbuotojas Employee { get; set; }

        // Populate Employee property
        public IActionResult OnGet(int id)
        {
           
            DBController db = new DBController(_config);
            dbc = db.ConnectToDB();
            worker = new List<Darbuotojas>();
            id2 = id;
            string query = @"
            SELECT t1.vardas, t1.pavarde, t1.gimimo_data,t1.id_Naudotojas
            FROM Darbuotojas t1 
            WHERE t1.id_Naudotojas=@id
            ";
            SqlCommand cmd = new SqlCommand(query, dbc);
            cmd.Parameters.AddWithValue("@id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {System.Diagnostics.Debug.WriteLine(id);
                    IDataRecord results = (IDataRecord)reader;
                    Darbuotojas reservation = new Darbuotojas
                    ( 
                        (string)results[0],
                        (string)results[1],
                        (DateTime)results[2],
                        (int)results[3]
                    );
                    worker.Add(reservation);
                }
                reader.Close();
            }
            dbc.Close();
            
            if (worker[0] == null)
            {
                return RedirectToPage("/NotFound");
            }
        Employee = worker[0];
            return Page();
        }
        public IActionResult OnPostSubmit(Darbuotojas employee, string Id)
        {
            System.Diagnostics.Debug.WriteLine(employee.Surname);
            System.Diagnostics.Debug.WriteLine(employee.Name);
            System.Diagnostics.Debug.WriteLine(employee.Birth);
            System.Diagnostics.Debug.WriteLine(Id);
                DBController db = new DBController(_config);
                dbc = db.ConnectToDB();
                string query = @"
            UPDATE Darbuotojas
            SET vardas=@name, pavarde=@surname, gimimo_data=@birth
            WHERE id_Naudotojas=@id
            ";
                SqlCommand cmd = new SqlCommand(query, dbc);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@surname", employee.Surname);
                cmd.Parameters.AddWithValue("@birth", employee.Birth);
                SqlDataReader reader = cmd.ExecuteReader();
                return RedirectToPage("/Admin/WorkerList");
     
        }
    }
}
