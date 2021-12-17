using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HotelServices.Pages
{
    public class WorkListModel : PageModel
    {
        private readonly ILogger<WorkListModel> _logger;
        public WorkListModel(ILogger<WorkListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
