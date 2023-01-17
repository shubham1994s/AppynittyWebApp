using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class MobileApps : Controller
    {

        private readonly AppynittyCommunicationContext _context;

        public MobileApps(AppynittyCommunicationContext context)
        {
            _context = context;
        }

        public IActionResult MobileApp()
        {
            return View();
        }

        public IActionResult CustomApp()
        {
            return View();
        }
        public IActionResult RealEstate()
        {
            return View();
        }
        public IActionResult Malls()
        {
            return View();
        }
        public IActionResult Restro()
        {
            return View();
        }
        public IActionResult Stayiinn()
        {
            return View();
        }
        public IActionResult Beauty()
        {
            return View();
        }
        public IActionResult VoterFirst()
        {
            return View();
        }
        public IActionResult Wheels()
        {
            return View();
        }
        public IActionResult Celeb()
        {
            return View();
        }
        public IActionResult Boutique()
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
                    TempData["value"] = "Saved";
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                //return RedirectToAction(nameof(Contact));
                return RedirectToAction("CustomApp", "MobileApps");
            }
            return RedirectToAction("CustomApp", "MobileApps", "form");
            //return RedirectToAction("Contact", "ContactUs", "form");
        }
    }
}
