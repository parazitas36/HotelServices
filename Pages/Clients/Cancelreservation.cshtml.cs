using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class CancelReservationModel : PageModel
    {
        private readonly ILogger<CancelReservationModel> _logger;

        public CancelReservationModel(ILogger<CancelReservationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
