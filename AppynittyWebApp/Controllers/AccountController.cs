using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
     


        public async Task<IActionResult> Details()
        {
         
                var Email = User.FindFirstValue(ClaimTypes.Email);
                //  string Email= HttpContext.Session.Id;
                if (Email == null)
                {
                return Redirect("/Identity/Account/Login");
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
