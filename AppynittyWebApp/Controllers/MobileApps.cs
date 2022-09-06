using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class MobileApps : Controller
    {
        public IActionResult MobileApp()
        {
            return View();
        }

        public IActionResult CustomApp()
        {
            return View();
        }
    }
}
