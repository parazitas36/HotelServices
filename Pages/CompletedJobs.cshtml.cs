using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelServices.Pages
{
    public class CompletedJobsModel : PageModel
    {
        private readonly ILogger<CompletedJobsModel> _logger;

        public CompletedJobsModel(ILogger<CompletedJobsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
