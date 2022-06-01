using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P2Pspeedrun.Models;
using P2Pspeedrun.Services;
using P2Pspeedrun.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P2Pspeedrun.Controllers
{
    public class HomeController : Controller
    {
        private AppService app;
        private UserService userService;

        public HomeController(AppService app, UserService userService)
        {
            this.app = app;
            this.userService = userService;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            IndexViewModel vm = new()
            {
                Messages = app.GetMessages(),
                Peers = app.Peers
            };

            return View(vm);
        }

        [HttpPost("post")]
        public IActionResult Post(Message m)
        {
            // here we control flow between App and User services.
            // we try avoid to make services dependent on each other (injected in each other)
            // rather controller has instances of different services and do it's thing – controlls flow of data and actions
            // ~> where ~> what ~> do ~> what -- in 3~5 lines 99% of time!
            // by the way - this is THE injection of user data into app service, so app service doesn't need to know about user service, and that's really really great – makes the code more separated and easier to change!
            app.NewMessage(m, userService.CurrentUser);

            return RedirectToAction("index");
        }
    }
}

