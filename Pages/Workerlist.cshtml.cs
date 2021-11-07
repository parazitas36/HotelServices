using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class WorkerlistModel : PageModel
    {
        private readonly ILogger<WorkerlistModel> _logger;

        public WorkerlistModel(ILogger<WorkerlistModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
