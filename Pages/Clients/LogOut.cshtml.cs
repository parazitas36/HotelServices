using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class LogOutCModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public LogOutCModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            //paima cookie value
            var role = Request.Cookies["role"];

            //istrina cookie
            Response.Cookies.Delete("role");

            return Redirect("../../");
        }
    }
}
