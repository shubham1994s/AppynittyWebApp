using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class ContactUs : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult franchise()
        {
            return View();
        }
    }
}
