using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Library.Data;
using My_Library.Entities;

namespace My_Library.Controllers
{
    public class BookTypeController : Controller
    {
        private readonly DatabaseContext _context;

        public BookTypeController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: BookTypeController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _context.BookTypes.ToListAsync();
            return View(model);
        }

        // GET: BookTypeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookType booktype)
        {
            try
            {
                _context.BookTypes.Add(booktype);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookTypeController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _context.BookTypes.Find(id);
            return View(model);
        }

        // POST: BookTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookType booktype)
        {
            try
            {
                _context.BookTypes.Update(booktype);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookTypeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model= _context.BookTypes.Find(id);
            return View();
        }

        // POST: BookTypeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, BookType booktype)
        {
            try
            {
                _context.BookTypes.Remove(booktype); 
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
