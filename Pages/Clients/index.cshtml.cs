using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class ClientindexModel : PageModel
    {
        private readonly ILogger<ClientindexModel> _logger;

        public ClientindexModel(ILogger<ClientindexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
