using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class ReservationsModel : PageModel
    {
        private readonly ILogger<ReservationsModel> _logger;

        public ReservationsModel(ILogger<ReservationsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
