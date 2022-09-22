using AppynittyWebApp.Models;
using AppynittyWebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var news = await _context.News.ToListAsync();
            return View(news);
        }

        // GET: NewsController
        //public ActionResult Index()
        //{
        //    return View();
        //}


        // GET: NewsController/Create
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
                var news = await _context.News.FindAsync(Id);

                if (news == null)
                {
                    return NotFound();
                }
                return View(news);
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
                return NotFound();
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
            NewsRplyVM NewsRplyDetail = new NewsRplyVM();
            List<NewsRplyDetailsIteam> ListNewsRplyItems = new List<NewsRplyDetailsIteam>();
            string StoredProc = "exec NewsReplyDetails " + "@Id = " + Id ;
            var data = _context.NewsReplyDetails.FromSqlRaw(StoredProc).ToList();

            if (data != null && data.Count > 0)
            {
                NewsRplyDetail.ListNewsRplyDetails = data.Select(x => new NewsRplyDetailsIteam()
                {
                    News_Id = x.News_Id,
                    Date = x.Date,
                    Name = x.Name,
                    Email = x.Email,
                    Mobile_No = x.Mobile_No,
                    Comment = x.Comment,
                    NewsTitle = x.NewsTitle
                })
               .ToList();
            }
                return View(NewsRplyDetail);
        }
    }
}
