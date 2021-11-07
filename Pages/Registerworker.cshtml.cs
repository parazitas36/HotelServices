using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class RegisterworkerModel : PageModel
    {
        private readonly ILogger<RegisterworkerModel> _logger;

        public RegisterworkerModel(ILogger<RegisterworkerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
