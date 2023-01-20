using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppynittyWebApp.Controllers
{
    public class NewsController : Controller
    {

        private readonly AppynittyCommunicationContext _context;

        public NewsController(AppynittyCommunicationContext context)
        {
            _context = context;
        }
        public async Task<ActionResult> IndexAsync()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
               // var news = await _context.News.ToListAsync();
                return View();
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
           
        }

        [HttpPost]
        public IActionResult LoadNewsData()
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
                var newsData = (from tempnews in _context.News.ToList() select tempnews);
                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    newsData = newsData.Where(m => m.NewsTitle.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = newsData.Count();
                //Paging   
                var data = newsData.Skip(skip).Take(pageSize).ToList();
                //Returning Json Data  
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception)
            {
                throw;
            }
        }


        // GET: NewsController/Create
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
                    var news = await _context.News.FindAsync(Id);

                    if (news == null)
                    {
                        return NotFound();
                    }
                    return View(news);
                }
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
        }
        // POST: NewsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int Id,
            [Bind("Id,NewsDate,NewsTitle,NewsEng,NewsMar,IsActive")] News NewsData)
        {
            bool IsnewsExist = false;

            News news = await _context.News.FindAsync(Id);

            if (news != null)
            {
                IsnewsExist = true;
            }
            else
            {
                news = new News();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    news.NewsDate = NewsData.NewsDate;
                    news.NewsTitle = NewsData.NewsTitle;
                    news.NewsEng = NewsData.NewsEng;
                    news.NewsMar = NewsData.NewsMar;
                    news.IsActive = NewsData.IsActive;

                    if (IsnewsExist)
                    {
                        _context.Update(news);
                    }
                    else
                    {
                        _context.Add(news);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(NewsData);
        }


        // GET: NewsController/Details/5
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
                var news = await _context.News.FirstOrDefaultAsync(m => m.Id == Id);
                if (news == null)
                {
                    return NotFound();
                }
                return View(news);
            }
            else
            {
                return Redirect("/Identity/Account/Login");
            }
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsController/Create
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

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsController/Edit/5
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

        // GET: NewsController/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var news = await _context.News.FirstOrDefaultAsync(m => m.Id == Id);

            return View(news);
        }

        // POST: NewsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            var news = await _context.News.FindAsync(Id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> NewSection(string Id, NewsVM model)
        {
          
            News news = await _context.News.Where(o=>o.IsActive==true).OrderByDescending(o => o.Id).FirstOrDefaultAsync();
            var snews = await _context.News.Where(o => o.IsActive == true).OrderByDescending(o => o.Id).ToListAsync();

            if (news == null)
            {
                return View(model);
            }
            int Ids = Convert.ToInt32(Id);
            if(Ids != 0)
            {
                news = await _context.News.Where(o => o.IsActive == true && o.Id== Ids).FirstOrDefaultAsync();
            }
            ViewBag.newslist = snews;
            ViewBag.ID = news.Id;
            model.NewsEng = news.NewsEng;
            model.NewsTitle = news.NewsTitle;
            model.Id = news.Id;
            model.NewsId = news.Id;
            return View(model);
        }
      
        
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> NewSection( NewsVM model)
        {


            NewsRply News = new NewsRply();
          
            if (ModelState.IsValid)
            {
                try
                {
                    News.Name = model.Name;
                    News.Email = model.Email;
                    News.MobileNo = model.MobileNo;
                    News.Comment = model.Comment;
                    News.NewsId = model.NewsId;
                    News.Date = DateTime.Now;
                                    
                   _context.Add(News);
                  
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("NewSection");
            }
            return View(model);
        }

        public IActionResult NewsReply(int Id)
        {
           

            var Email = User.FindFirstValue(ClaimTypes.Email);
            if (Email != null)
            {
                TempData["Email"] = Email;
                TempData["NewsId"] = Id;
                //NewsRplyVM NewsRplyDetail = new NewsRplyVM();
                //List<NewsRplyDetailsIteam> ListNewsRplyItems = new List<NewsRplyDetailsIteam>();
                //string StoredProc = "exec NewsReplyDetails " + "@Id = " + Id;
                //var data = _context.NewsReplyDetails.FromSqlRaw(StoredProc).ToList();

                //if (data != null && data.Count > 0)
                //{
                //    NewsRplyDetail.ListNewsRplyDetails = data.Select(x => new NewsRplyDetailsIteam()
                //    {
                //        News_Id = x.News_Id,
                //        Date = x.Date,
                //        Name = x.Name,
                //        Email = x.Email,
                //        Mobile_No = x.Mobile_No,
                //        Comment = x.Comment,
                //        NewsTitle = x.NewsTitle
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
        public IActionResult LoadNewsReplyData(int Id)
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

                string StoredProc = "exec NewsReplyDetails " + "@Id = " + Id;
                // getting all Customer data  
                var newsreplyData = (from tempnewsreply in _context.NewsReplyDetails.FromSqlRaw(StoredProc).ToList()
                                    select tempnewsreply);

                //NewsRplyVM NewsRplyDetail = new NewsRplyVM();

                //if (newsreplyData != null)
                //{
                //    NewsRplyDetail.ListNewsRplyDetails = newsreplyData.Select(x => new NewsRplyDetailsIteam()
                //    {
                //        News_Id = x.News_Id,
                //        Date = x.Date,
                //        Name = x.Name,
                //        Email = x.Email,
                //        Mobile_No = x.Mobile_No,
                //        Comment = x.Comment,
                //        NewsTitle = x.NewsTitle
                //    })
                //   .ToList();
                //}
                //Sorting  
                //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                //{
                //    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                //}
                //Search  
                if (!string.IsNullOrEmpty(searchValue))
                {
                    newsreplyData = newsreplyData.Where(m => m.Name.ToLower().Contains(searchValue.ToLower()) 
                                                        || m.Email.ToLower().Contains(searchValue.ToLower())
                                                        || m.Comment.ToLower().Contains(searchValue.ToLower())
                                                        || m.NewsTitle.ToLower().Contains(searchValue.ToLower()));
                }

                //total number of rows counts   
                recordsTotal = newsreplyData.Count();
                //Paging   
                var data = newsreplyData.Skip(skip).Take(pageSize).ToList();
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
