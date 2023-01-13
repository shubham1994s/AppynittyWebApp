using AppynittyWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class DemoGridController : Controller
    {
        private readonly AppynittyCommunicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DemoGridController(AppynittyCommunicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowGrid()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoadData()
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

                string StoredProc = "exec CarrersDetails ";
                // getting all Customer data  
                var customerData = (from tempcustomer in _context.CarrersDetails.FromSqlRaw(StoredProc).ToList()
                                    select tempcustomer);
                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    customerData = customerData.Where(m => m.JobTitle.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = customerData.Count();
                //Paging   
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [HttpPost]
        public JsonResult GetEmployeeList()
        {
            int totalRecord = 0;
            int filterRecord = 0;
            var draw = Request.Form["draw"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");
            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");
            string StoredProc = "exec CarrersDetails ";

            var data = _context.CarrersDetails.FromSqlRaw(StoredProc).ToList();
            //get total count of data in table
            totalRecord = data.Count();
            // search data when search value found
            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    data = data.Where(x => x.JobTitle.ToLower().Contains(searchValue.ToLower()));
            //}
            // get total count of records after search
            filterRecord = data.Count();
            //sort data
            //if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection)) data = data.OrderBy(sortColumn + " " + sortColumnDirection);
            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();
            var returnObj = new
            {
                draw = draw,
                recordsTotal = totalRecord,
                recordsFiltered = filterRecord,
                data = empList
            };

            return Json(new { draw = draw, recordsFiltered = filterRecord, recordsTotal = totalRecord, data = data });
        }
    }
}

