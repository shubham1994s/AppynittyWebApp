﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class DigitalSolution : Controller
    {
        public IActionResult leadGeneration()
        {
            return View();
        }
        public IActionResult leadManagement()
        {
            return View();
        }
        public IActionResult EmailManagement()
        {
            return View();
        }
        public IActionResult SalesIntelligence()
        {
            return View();
        }
    }
}
