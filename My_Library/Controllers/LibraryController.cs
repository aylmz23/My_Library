using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Library.Data;
using My_Library.Entities;

namespace My_Library.Controllers
{
    public class LibraryController : Controller
    {
        //[Area("Library")]
        private readonly DatabaseContext _context;

        public LibraryController(DatabaseContext context)
        {
            _context = context;
        }
        // GET: LibraryController1
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _context.Libraries.Include(c => c.Status).Include(b => b.BookType).ToListAsync();
            return View(model);
        }

        // GET: LibraryController1/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.StatusId = new SelectList(_context.Status.ToList(), "Id", "status");
            ViewBag.BookTypeId = new SelectList(_context.BookTypes.ToList(), "Id", "Name");
            var model = _context.Libraries.Find(id);
            return View(model);
        }

        // GET: LibraryController1/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(_context.Status.ToList(), "Id", "status");
            ViewBag.BookTypeId = new SelectList(_context.BookTypes.ToList(), "Id", "Name");
            return View();
        }

        // POST: LibraryController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Library library, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    string fName = library.Name + "-" + library.Author;
                    string fileName = string.Format(fName.ToLower().Replace('ı', 'i').Replace('ü', 'u').Replace('ş', 's').Replace('ç', 'c').Replace('ğ', 'g').Replace('ö', 'o').Replace(' ', '_').Replace('İ', 'I').Trim().ToString());
                    var extName = Path.GetExtension(Image.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", fileName + extName);
                    using var stream = new FileStream(path, FileMode.Create);
                    Image.CopyTo(stream);
                    library.Image = fileName + extName;
                }
                _context.Libraries.Add(library);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibraryController1/Edit/5
        public ActionResult Edit(int id)
        {

            var model = _context.Libraries.Find(id);
            ViewBag.StatusId = new SelectList(_context.Status.ToList(), "Id", "status");
            ViewBag.BookTypeId = new SelectList(_context.BookTypes.ToList(), "Id", "Name");
            return View(model);
        }

        // POST: LibraryController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Library library, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    string fName = library.Name + "-" + library.Author;
                    string fileName = string.Format(fName.ToLower().Replace('ü', 'u').Replace('ş', 's').Replace('ç', 'c').Replace('ğ', 'g').Replace('ö', 'o').Replace(' ', '_').Replace('İ', 'I').Trim().ToString());
                    var extName = Path.GetExtension(Image.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Img", fileName + extName);
                    using var stream = new FileStream(path, FileMode.Create);
                    Image.CopyTo(stream);
                    library.Image = fileName + extName;
                }
                _context.Libraries.Update(library);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibraryController1/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _context.Libraries.SingleOrDefaultAsync(c => c.Id == id);
            ViewBag.StatusId = new SelectList(_context.Status.ToList(), "Id", "status");
            ViewBag.BookTypeId = new SelectList(_context.BookTypes.ToList(), "Id", "Name");
            return View(model);
        }

        // POST: LibraryController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Library libray)
        {
            try
            {
                _context.Libraries.Remove(libray);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Views()
        {
            var model = _context.Libraries.Include(c => c.Status).Include(b => b.BookType).ToList();
            return View(model);
        }

        public ActionResult Search(string SearchName, string SearchAuthor, string SearchStatus, string SearchBookType)
        {
            ViewData["Search_Name"] = SearchName;
            ViewData["Search_Author"] = SearchAuthor;
            ViewData["Search_Status"] = SearchStatus;
            ViewData["Search_BookType"] = SearchBookType;
            var books = from book in _context.Libraries.Include(c => c.Status).Include(b => b.BookType).ToList() select book;
            if (!String.IsNullOrEmpty(SearchName))
            {
                books = books.Where(book => book.Name.Contains(SearchName));
            }
            if (!String.IsNullOrEmpty(SearchAuthor))
            {
                books = books.Where(book => book.Author.Contains(SearchAuthor));
            }
            if (!String.IsNullOrEmpty(SearchStatus))
            {
                books = books.Where(book => book.Status.status.Contains(SearchStatus));
            }
            if (!String.IsNullOrEmpty(SearchBookType))
            {
                books = books.Where(book => book.BookType.Name.Contains(SearchBookType));
            }
            return View(books);
        }
    }
}
