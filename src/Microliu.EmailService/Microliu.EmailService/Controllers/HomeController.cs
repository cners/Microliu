using Microliu.EmailService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Microliu.EmailService.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var currentHost = Request.HttpContext.Connection.LocalIpAddress.MapToIPv4().ToString() + ":" +
               Request.HttpContext.Connection.LocalPort;
            ViewBag.Host = currentHost;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
