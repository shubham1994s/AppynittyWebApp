using AppynittyWebApp.Models;
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
            if(NewsData.IsActive == null)
            {
                NewsData.IsActive = false;
            }
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
    }
}
