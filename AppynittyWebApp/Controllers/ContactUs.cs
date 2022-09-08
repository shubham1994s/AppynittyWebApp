using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class ContactUs : Controller
    {
        private readonly AppynittyCommunicationContext _context;

        public ContactUs(AppynittyCommunicationContext context)
        {
            _context = context;
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult franchise()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactU model)
        {


            ContactU Contact = new ContactU();

            if (ModelState.IsValid)
            {
                try
                {
                    Contact.Name = model.Name;
                    Contact.Email = model.Email;
                    Contact.MobileNo = model.MobileNo;
                    Contact.Country = model.Country;
                    Contact.City = model.City;
                    Contact.Business = model.Business;
                    Contact.Date = DateTime.Now;
                   
                   
                    _context.Add(Contact);

                    await _context.SaveChangesAsync();
                    TempData["data"] = "Success";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                //return RedirectToAction(nameof(Contact));
                return RedirectToAction("Contact", "ContactUs", "form");
            }
            return View(model);
            //return RedirectToAction("Contact", "ContactUs", "form");
        }

    }
}
