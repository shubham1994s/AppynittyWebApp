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
using System.Security.Claims;
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
        public  IActionResult Index()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
                //var blogs = await _context.Blogs.ToListAsync();
                return View();
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
           
        }

        [HttpPost]
        public IActionResult LoadBlogData()
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
                var blogsData = (from tempblogs in _context.Blogs.ToList() select tempblogs);
                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    blogsData = blogsData.Where(m => m.BlogsTitle.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = blogsData.Count();
                //Paging   
                var data = blogsData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }
        // GET: BlogController/Create
        public async Task<ActionResult> AddOrEdit(int? Id)
        {
            

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
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
            else
            {
                return Redirect("/Identity/Account/Login");
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
            

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
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
            else
            {
                return Redirect("/Identity/Account/Login");
            }
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
            var blogs = await _context.Blogs.Where(x=>x.IsActive==true).ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> BlogSection(string Id, BlogViewModel model)
        {

            Blog Blogs = await _context.Blogs.Where(o => o.IsActive == true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            var SBlogs = await _context.Blogs.Where(o => o.IsActive == true).OrderByDescending(o => o.Id).ToListAsync();
            
            if (Blogs == null)
            {
                return NotFound();
            }
            int Ids = Convert.ToInt32(Id);
            if (Ids != 0)
            {
                Blogs = await _context.Blogs.Where(o => o.IsActive == true && o.Id == Ids).FirstOrDefaultAsync();
            }
            ViewBag.Bloglist = SBlogs;
            ViewBag.ID = Blogs.Id;
            model.BlogsEng = Blogs.BlogsEng;
            model.BlogsTitle = Blogs.BlogsTitle;
            model.Id = Blogs.Id;
            model.BlogId = Blogs.Id;
            model.BFileName = Blogs.FileName;
            return View(model);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> BlogSection(BlogViewModel model)
        {


            BlogRply Blog = new BlogRply();

           
                try
                {
                    Blog.Name = model.Name;
                    Blog.Email = model.Email;
                    Blog.MobileNo = model.MobileNo;
                    Blog.Comment = model.Comment;
                    Blog.BlogId = model.BlogId;
                    Blog.Date = DateTime.Now;

                    _context.Add(Blog);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("BlogSection");
      
            return View(model);
        }

        public IActionResult BlogReply(int Id)
        {

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
                TempData["BlogId"] = Id;
                //BlogReplyVM NewsRplyDetail = new BlogReplyVM();
                //List<BlogRplyDetailsIteam> ListNewsRplyItems = new List<BlogRplyDetailsIteam>();

                //var data = _context.BlogRplies.Where(a => a.BlogId == Id).ToList();

                //if (data != null && data.Count > 0)
                //{
                //    NewsRplyDetail.ListNewsRplyDetails = data.Select(x => new BlogRplyDetailsIteam()
                //    {
                //        Blog_Id = x.BlogId,
                //        Date = x.Date,
                //        Name = x.Name,
                //        Email = x.Email,
                //        Mobile_No = x.MobileNo,
                //        Comment = x.Comment,
                //        BlogTitle = _context.Blogs.Where(c => c.Id == x.BlogId).Select(c => c.BlogsTitle).FirstOrDefault()
                //    })
                //   .ToList();
                //}

                return View();
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
        }

        [HttpPost]
        public IActionResult LoadBlogReplyData(int Id)
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

                string StoredProc = "exec BlogReplyDetails " + "@Id = " + Id;
                // getting all Customer data  
                var blogreplyData = (from tempblogsreply in _context.BlogReplyDetails.FromSqlRaw(StoredProc).ToList()
                                     select tempblogsreply);


                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    blogreplyData = blogreplyData.Where(m => m.Name.ToLower().Contains(searchValue.ToLower())
                                                        || m.Email.ToLower().Contains(searchValue.ToLower())
                                                        || m.Comment.ToLower().Contains(searchValue.ToLower())
                                                        || m.BlogTitle.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = blogreplyData.Count();
                //Paging   
                var data = blogreplyData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
