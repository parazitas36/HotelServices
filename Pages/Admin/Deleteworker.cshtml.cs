using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class DeleteworkerModel : PageModel
    {
        private readonly ILogger<DeleteworkerModel> _logger;

        public DeleteworkerModel(ILogger<DeleteworkerModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
