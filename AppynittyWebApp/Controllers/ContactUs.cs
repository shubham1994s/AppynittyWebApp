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
    public class ContactUs : Controller
    {
        private readonly AppynittyCommunicationContext _context;

        public ContactUs(AppynittyCommunicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
               // var news = await _context.ContactUs.ToListAsync();
                return View();
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
            
        }

        [HttpPost]
        public IActionResult LoadContactEnquiryData()
        {
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count  
                var start = Request.Form["start"].FirstOrDefault();

                // Paging Length 10,20  
                var length = Request.Form["length"].FirstOrDefault();

                // Sort Column Name  
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();

                // Sort Column Direction (asc, desc)  
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();

                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault();

                //Paging Size (10, 20, 50,100)  
                int pageSize = length != null ? Convert.ToInt32(length) : 0;

                int skip = start != null ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                
                // getting all Customer data  
                var contactData = (from tempcontact in _context.ContactUs.ToList() select tempcontact);


                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    contactData = contactData.Where(m => m.Name.ToLower().Contains(searchValue.ToLower())
                                                        || m.Email.ToLower().Contains(searchValue.ToLower())
                                                        || m.Name.ToLower().Contains(searchValue.ToLower())
                                                        || m.MobileNo.ToLower().Contains(searchValue.ToLower())
                                                        || m.Country.ToLower().Contains(searchValue.ToLower())
                                                        || m.City.ToLower().Contains(searchValue.ToLower())
                                                        || m.Business.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = contactData.Count();
                //Paging   
                var data = contactData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

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
                    TempData["value"] = "Saved";
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
