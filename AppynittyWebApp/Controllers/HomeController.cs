using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            //return Redirect("Identity/Account/Login");
            return View();
        }
        public IActionResult AboutUS()
        {

            //return Redirect("Identity/Account/Login");
            return View();
        }
        public IActionResult E_Governance()
        {

            //return Redirect("Identity/Account/Login");
            return View();
        }
        public IActionResult WasteManagement()
        {

            //return Redirect("Identity/Account/Login");
            return View();
        }
        public IActionResult Urban_Services()
        {

            //return Redirect("Identity/Account/Login");
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
