﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Pages
{
    public class DeleteAccModel : PageModel
    {
        private readonly ILogger<DeleteAccModel> _logger;

        public DeleteAccModel(ILogger<DeleteAccModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
