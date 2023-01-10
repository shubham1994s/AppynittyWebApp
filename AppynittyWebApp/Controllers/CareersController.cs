using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class CareersController : Controller
    {
        private readonly AppynittyCommunicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CareersController(AppynittyCommunicationContext context, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //var connectionString = Configuration.GetConnectionString("AppynittyWebAppContextConnection");

        private SqlConnection con;
        private void connection()
        {
            string constring = Configuration.GetConnectionString("AppynittyWebAppContextConnection").ToString();
            con = new SqlConnection(constring);
        }
        public ActionResult Index()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if(Email != null)
            {
                TempData["Email"] = Email;

                CarrersDetailsVM CVDetail = new CarrersDetailsVM();
                List<CarrersDetailsIteam> ListAppEmpCVItems = new List<CarrersDetailsIteam>();
                string StoredProc = "exec CarrersDetails ";
                var data = _context.CarrersDetails.FromSqlRaw(StoredProc).ToList();

                if (data != null && data.Count > 0)
                {
                    CVDetail.ListCarrersDetails = data.Select(x => new CarrersDetailsIteam()
                    {
                        Id = x.Id,
                        Date = x.Date,
                        JobTitle = x.JobTitle,
                        IsActive = x.IsActive,
                        CareersCount = x.CareersCount
                    })
                   .ToList();
                }
                return View(CVDetail);


                //var career = await _context.Careers.ToListAsync();
                //return View(career);
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: CareersController/Create
        public async Task<ActionResult> AddOrEdit(int? Id)
        {
           

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
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
            else
            {
                return Redirect("/Identity/Account/Login");
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
          

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
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
            else
            {
                return Redirect("/Identity/Account/Login");
            }
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
                    Job.ViewStatus = false;
                    _context.Add(Job);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                TempData["value"] = "Saved";
                return RedirectToAction("OpenPosition");
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

        public async Task<IActionResult> ViewCV(int Id)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
                AppliedEmpVM AppEmpCVDetail = new AppliedEmpVM();
                List<AppEmpCVDetailsIteam> ListAppEmpCVItems = new List<AppEmpCVDetailsIteam>();
                string StoredProc = "exec AppEmpCVDetails " + "@Id = " + Id;
                var data = _context.AppEmpCVDetails.FromSqlRaw(StoredProc).ToList();

                //AppliedEmp career = await _context.AppliedEmps.FindAsync(Id);
                

                //if (career != null)
                //{
                //    career.ViewStatus = true;
                //    _context.Update(career);

                //    await _context.SaveChangesAsync();
                //}

                connection();
                string query = "UPDATE Applied_Emp SET View_Status = '" + true + "' WHERE Careers_Id = " + Id + " And View_Status = 0";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int i = cmd.ExecuteNonQuery();
                con.Close();


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
            else
            {
                return Redirect("/Identity/Account/Login");
            }
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
