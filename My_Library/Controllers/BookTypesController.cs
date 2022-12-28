using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Library.Data;
using My_Library.Entities;

namespace My_Library.Controllers
{
    public class BookTypesController : Controller
    {
        private readonly DatabaseContext _context;

        public BookTypesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: BookTypes
        public async Task<IActionResult> Index()
        {
              return _context.BookTypes != null ? 
                          View(await _context.BookTypes.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.BookTypes'  is null.");
        }

        // GET: BookTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookTypes == null)
            {
                return NotFound();
            }

            var bookType = await _context.BookTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookType == null)
            {
                return NotFound();
            }

            return View(bookType);
        }

        // GET: BookTypes/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: BookTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookType);
        }
        //Create2
        public async Task<IActionResult> Create2([Bind("Id,Name")] BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","Library");
            }
            return View(bookType);
        }
        // GET: BookTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BookTypes == null)
            {
                return NotFound();
            }

            var bookType = await _context.BookTypes.FindAsync(id);
            if (bookType == null)
            {
                return NotFound();
            }
            return View(bookType);
        }

        // POST: BookTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] BookType bookType)
        {
            if (id != bookType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookTypeExists(bookType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookType);
        }

        // GET: BookTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BookTypes == null)
            {
                return NotFound();
            }

            var bookType = await _context.BookTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookType == null)
            {
                return NotFound();
            }

            return View(bookType);
        }

        // POST: BookTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookTypes == null)
            {
                return Problem("Entity set 'DatabaseContext.BookTypes'  is null.");
            }
            var bookType = await _context.BookTypes.FindAsync(id);
            if (bookType != null)
            {
                _context.BookTypes.Remove(bookType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookTypeExists(int id)
        {
          return (_context.BookTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
