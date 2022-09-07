using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class CareersController : Controller
    {
        private readonly AppynittyCommunicationContext _context;

        public CareersController(AppynittyCommunicationContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> IndexAsync()
        {
            var career = await _context.Careers.ToListAsync();
            return View(career);
        }

        // GET: CareersController/Create
        public async Task<ActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Create Job" : "Edit Job";
            ViewBag.IsEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                var careers = await _context.Careers.FindAsync(Id);

                if (careers == null)
                {
                    return NotFound();
                }
                return View(careers);
            }
        }
        // POST: CareersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int Id,
            [Bind("Id,Date,JobTitle,MinExp,MaxExp,MinSalary,MaxSalary,JobLocation,JobDesc,IsActive")] Career CareerData)
        {
            bool IsnewsExist = false;

            Career career = await _context.Careers.FindAsync(Id);

            if (career != null)
            {
                IsnewsExist = true;
            }
            else
            {
                career = new Career();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    career.Date = CareerData.Date;
                    career.JobTitle = CareerData.JobTitle;
                    career.MinExp = CareerData.MinExp;
                    career.MaxExp = CareerData.MaxExp;
                    career.MinSalary = CareerData.MinSalary;
                    career.MaxSalary = CareerData.MaxSalary;
                    career.JobDesc = CareerData.JobDesc;
                    career.JobLocation = CareerData.JobLocation;
                    career.IsActive = CareerData.IsActive;

                    if (IsnewsExist)
                    {
                        _context.Update(career);
                    }
                    else
                    {
                        _context.Add(career);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(CareerData);
        }


        // GET: CareersController/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var careers = await _context.Careers.FirstOrDefaultAsync(m => m.Id == Id);
            if (careers == null)
            {
                return NotFound();
            }
            return View(careers);
        }

        // GET: CareersController/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var career = await _context.Careers.FirstOrDefaultAsync(m => m.Id == Id);

            return View(career);
        }

        // POST: CareersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var career = await _context.Careers.FindAsync(Id);
            _context.Careers.Remove(career);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> OpenPosition(CareersVM model)
        {

            var careersData = await _context.Careers.Where(o => o.IsActive == true).OrderByDescending(o => o.Id).ToListAsync();

            ViewBag.careerslist = careersData;

            return View(model);
        }
    }
}
