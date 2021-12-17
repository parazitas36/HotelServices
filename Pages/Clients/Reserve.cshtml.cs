using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class ReserveModel : PageModel
    {
        private readonly ILogger<ReserveModel> _logger;

        public ReserveModel(ILogger<ReserveModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
