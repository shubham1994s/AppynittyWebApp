using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class CareersController : Controller
    {
        private readonly AppynittyCommunicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CareersController(AppynittyCommunicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
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

        [HttpGet]
        public async Task<IActionResult> CVSection()
        {
            ViewData["JobList"] = new SelectList(_context.Careers, "Id", "JobTitle");

            var JobListData = await _context.Careers.Where(o => o.IsActive == true).OrderByDescending(o => o.Id).ToListAsync();
            ViewBag.Joblist = JobListData;
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CVSection(CareersVM model)
        {


            AppliedEmp Job = new AppliedEmp();

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    Job.CareersId = model.Careers_Id;
                    Job.Name = model.Name;
                    Job.Email = model.Email;
                    Job.MobileNo = model.MobileNo;
                    Job.CurrentLocation = model.CurrentLocation;
                    Job.TotExp = model.TotExp;
                    Job.Filename = uniqueFileName;
                    Job.Tac = model.Tac;
                    Job.Date = DateTime.Now;

                    _context.Add(Job);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(OpenPosition));
            }
            return View(model);
        }

        private string UploadedFile(CareersVM model)
        {
            string uniqueFileName = null;

            if (model.FileName != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "UploadedCV");

                //var guid = Guid.NewGuid().ToString().Split('-');
                //uniqueFileName = DateTime.Now.ToString("MMddyyyymmss") + "_" + guid + ".jpg";

                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.FileName.FileName;

                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.FileName.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult ViewCV(int Id)
        {
            AppliedEmpVM AppEmpCVDetail = new AppliedEmpVM();
            List<AppEmpCVDetailsIteam> ListAppEmpCVItems = new List<AppEmpCVDetailsIteam>();
            string StoredProc = "exec AppEmpCVDetails " + "@Id = " + Id;
            var data = _context.AppEmpCVDetails.FromSqlRaw(StoredProc).ToList();

            if (data != null && data.Count > 0)
            {
                AppEmpCVDetail.ListAppEmpCVDetails = data.Select(x => new AppEmpCVDetailsIteam()
                {
                    Careers_Id = x.Careers_Id,
                    Date = x.Date,
                    Name = x.Name,
                    Email = x.Email,
                    Mobile_No = x.Mobile_No,
                    Current_Location = x.Current_Location,
                    Tot_Exp = x.Tot_Exp,
                    Filename = x.Filename,
                    TAC = x.TAC,
                    JobTitle = x.JobTitle
                })
               .ToList();
            }
            return View(AppEmpCVDetail);
        }

        public FileResult DownloadFile(string fileName)
        {
            //Build the File Path.
            string path = Path.Combine(this.webHostEnvironment.WebRootPath, "UploadedCV/") + fileName;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }

        public IActionResult TermsAndConditions()
        {
            return View();
        }
    }
}
