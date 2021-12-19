using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class RegisterWorkerindexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public int msg = 0;

        public RegisterWorkerindexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int id)
        {
            msg = id;
        }
    }
}
