﻿using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppynittyCommunicationContext _context;

        public AccountController(AppynittyCommunicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View();
        }
     


        public async Task<IActionResult> Details(string Email)
        {
            if (Email == null)
            {
                return NotFound();
            }
            var employee = await _context.AspNetUsers.FirstOrDefaultAsync(m => m.Email == Email);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

    }
}