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
        public void OnPost()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/index");
        }
    }
}
