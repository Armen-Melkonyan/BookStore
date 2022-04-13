using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyTask.BookStore.Models;
using MyTask.BookStore.Repository;
using MyTask.BookStore.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyTask.BookStore.Controllers
{
    [Route("[Controller]/[action]")]
    public class HomeController : Controller
    {
        //Create instance of configuration to use appsettings.json in controller
        private readonly NewBookAlertModel newBookAlertModel;
        private readonly NewBookAlertModel thirdPartyBook;
        private readonly IMessageRepository messageRepository;
        private readonly IUserService userService;
        private readonly IEmailService emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertModel> newBookAlertModel,
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService)
        {
            this.newBookAlertModel = newBookAlertModel.Get("InternalBook");
            this.thirdPartyBook = newBookAlertModel.Get("ThirdPartyBook");
            this.messageRepository = messageRepository;
            this.userService = userService;
            this.emailService = emailService;
            this.userService = userService;
        }

        //Use this property to set title of layout page
        [ViewData]
        public string Title { get; set; }

        //home page
        [Route("~/")]
        public ViewResult Index()
        {
            Title = "Home Page";

            //UserEmailOptions options = new UserEmailOptions()
            //{
            //    ToEmail = new List<string> { "test@gmail.com" }
            //};

            //await emailService.SendtestEmail(options);

            //get appSettings hear
            bool displayNewBookAlert = newBookAlertModel.DisplayNewBookAlert;
            bool thirdPartyBook = newBookAlertModel.DisplayNewBookAlert;
            //var value = messageRepository.GatName();
            //bool DisplayNewBookAlert = newBookAlertModel.DisplayNewBookAlert;
            //var DisplayNewBookAlert = _configuration.GetValue<bool>("NewBookAlert:DisplayNewBookAlert");
            //var Alert = _configuration.GetValue<string>("NewBookAlert:Alert");

            //var appName = _configuration["Test2"];
            //get object from appSettings
            //var obj = _configuration["InfoObject:item1"];
            //var obj2 = _configuration["InfoObject:item2"];
            //var obj3 = _configuration["InfoObject:item3:subItem1"];
            //var obj4 = _configuration["InfoObject:item3:subItem2"];

            //using user service to get user id
            var userId = userService.GetUserId();
            var isAuthenticated = userService.UserAuthenticated();

            ViewBag.No = 123;
            return View();
        }

        public ViewResult ContactUs()
        {
            Title = "Contact Us";
            return View();
        }

        public ViewResult AboutUs(int id, string name)
        {
            Title = "About Us";
            return View();
        }

        public IActionResult Privacy()
        {
            Title = "Privacy";
            return View();
        }
        
    }
}
