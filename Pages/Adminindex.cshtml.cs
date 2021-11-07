using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class AdminindexModel : PageModel
    {
        private readonly ILogger<AdminindexModel> _logger;

        public AdminindexModel(ILogger<AdminindexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
