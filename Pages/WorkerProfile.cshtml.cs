using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelServices.Pages
{
    public class WorkerProfileModel : PageModel
    {
        private readonly ILogger<WorkerProfileModel> _logger;

        public WorkerProfileModel(ILogger<WorkerProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
