using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using P2Pspeedrun.Models;
using P2Pspeedrun.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace P2Pspeedrun.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private AppService app;

        public ApiController(AppService app)
        {
            this.app = app;
        }

        [HttpPost("message/receive")]
        public IActionResult Receive([FromBody] MessageProtocol mp)
        {
            string ip = HttpContext.Connection.RemoteIpAddress.ToString();

            app.ReceiveMessage(mp.Message);
            app.HavePeer(ip, mp.Client);

            return Ok();
        }
    }
}

