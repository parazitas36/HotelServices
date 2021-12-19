using HotelServices.Models;
using HotelServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelServices.Controllers
{
    public class MailController : ControllerBase
    {
        private readonly IMailService mailService;
        public MailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        public async void SendMail(MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}
