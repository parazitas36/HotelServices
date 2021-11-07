using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelServices.Pages
{
    public class WorkerProfileEditModel : PageModel
    {
        private readonly ILogger<WorkerProfileEditModel> _logger;
        public WorkerProfileEditModel(ILogger<WorkerProfileEditModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
