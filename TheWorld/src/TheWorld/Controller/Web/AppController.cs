using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Extensions.Configuration;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controller.Web
{
    public class AppController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMailService _mailService;
        private readonly IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel viewModel)
        {
            if(viewModel.Email.Contains("aol.com")) ModelState.AddModelError("", "We don't support aol addresses");
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], viewModel.Email, "From TheWorld", viewModel.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
