using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HotelServices.Pages.Admin
{
    public class LogOutModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost()
        {
            HttpContext.Session.Clear();
            Response.Redirect("/index");
        }
    }
}
