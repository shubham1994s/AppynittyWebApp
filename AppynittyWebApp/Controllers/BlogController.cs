using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class BlogController : Controller
    {

        private readonly AppynittyCommunicationContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BlogController(AppynittyCommunicationContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<ActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        // GET: BlogController/Create
        public async Task<ActionResult> AddOrEdit(int? Id)
        {
            ViewBag.PageName = Id == null ? "Create News" : "Edit News";
            ViewBag.IsEdit = Id == null ? false : true;
          
            if (Id == null)
            {
                return View();
            }
            else
            {
                var blogs = await _context.Blogs.FindAsync(Id);
                ViewBag.Filename = blogs.FileName;
                if (blogs == null)
                {
                    return NotFound();
                }
                return View(blogs);
            }
        }
        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit (BlogViewModel model)
        {
            bool IsnewsExist = false;

          

            Blog blogs = await _context.Blogs.FindAsync(model.Id);

            if (blogs != null)
            {
                IsnewsExist = true;
            }
            else
            {
                blogs = new Blog();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string uniqueFileName = UploadedFile(model);

                    blogs.BlogsDate = model.BlogsDate;
                    blogs.BlogsTitle = model.BlogsTitle;
                    blogs.BlogsEng = model.BlogsEng;
                    blogs.BlogsMar = model.BlogsMar;
                    blogs.IsActive = model.IsActive;
                    blogs.FileName = uniqueFileName; 

                    if (IsnewsExist)
                    {
                        _context.Update(blogs);
                    }
                    else
                    {
                        _context.Add(blogs);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private string UploadedFile(BlogViewModel model)
        {
            string uniqueFileName = null;

            if (model.FileName != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "BlogImages");

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

        // GET: BlogController/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var blogs = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == Id);
            if (blogs == null)
            {
                return NotFound();
            }
            return View(blogs);
        }

      

        // GET: BlogController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BlogController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BlogController/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var blogs = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == Id);

            return View(blogs);
        }

        // POST: BlogController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var blogs = await _context.Blogs.FindAsync(Id);
            _context.Blogs.Remove(blogs);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> OurBlog()
        {
            return View();
        }
    }
}
