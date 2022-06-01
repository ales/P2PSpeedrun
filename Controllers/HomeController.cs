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
        private readonly AppService app;
        private readonly UserService userService;

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
            app.NewMessage(m, userService.CurrentUser);

            return RedirectToAction("index");
        }
    }
}

