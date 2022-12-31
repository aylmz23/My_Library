using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Library.Data;
using My_Library.Models;

namespace My_Library.Controllers
{
    public class Excel : Controller
    {
        private readonly DatabaseContext _context;
        public Excel(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult ExportExcel()
        {
            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("Book List");
                worksheet.Cell(1, 1).Value = "Book Id";
                worksheet.Cell(1, 2).Value = "Book Name";
                worksheet.Cell(1, 3).Value = "Book Author";
                worksheet.Cell(1, 4).Value = "Book Type";
                worksheet.Cell(1, 5).Value = "Book Status";
                worksheet.Cell(1, 6).Value = "Book Where";

                int BookRowCount = 2;

                foreach (var item in GetBookList())
                {
                    worksheet.Cell(BookRowCount, 1).Value = item.bId;
                    worksheet.Cell(BookRowCount, 2).Value = item.bName;
                    worksheet.Cell(BookRowCount, 3).Value = item.bAuthor;
                    worksheet.Cell(BookRowCount, 4).Value = item.bType;
                    worksheet.Cell(BookRowCount, 5).Value = item.bStatus;
                    worksheet.Cell(BookRowCount, 6).Value = item.bWhere;

                    BookRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "bookList.xlsx");
                }
            }
        }
        public List<BookModel> GetBookList()
        {
            var model = _context.Libraries.Include(c => c.Status).Include(b => b.BookType).ToList();
            List<BookModel> bm = new List<BookModel>();
            using (_context)
            {
                bm = model.Select(x => new BookModel
                {
                    bId = x.Id,
                    bName = x.Name,
                    bAuthor = x.Author,
                    bType = x.BookType.Name,
                    bStatus = x.Status.status,
                    bWhere = x.Where,

                }).ToList();

            };
            return bm;
        }
        public IActionResult BookListExcel()
        {
            var model =  _context.Libraries.Include(c => c.Status).Include(b => b.BookType).ToList();
            return View(model);
        }

    }
    }
