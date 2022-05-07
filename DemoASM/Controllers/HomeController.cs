using DemoASM.Data;
using DemoASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DemoASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DemoASMContext _context;
        private readonly int _recordsPerPage = 8;

        public HomeController(ILogger<HomeController> logger, DemoASMContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(int id = 0, string SearchString = "")
        {
            ViewData["CurrentFilter"] = SearchString;
            var books = from s in _context.Books select s;
            // var students1 = await _context.Students.Where(s => s.LastName.Contains(SearchString)).ToListAsync();
            books = books.Where(s => s.Title.Contains(SearchString) ||  s.Category.Contains(SearchString))
                 .Skip(id * _recordsPerPage)  //Offset SQL
                .Take(_recordsPerPage); ;

            int numberOfRecords = await _context.Books.CountAsync();     //Count SQL
            int numberOfPages = (int)Math.Ceiling((double)numberOfRecords / _recordsPerPage);
            ViewBag.numberOfPages = numberOfPages;
            ViewBag.currentPage = id;

/*            var appDevDemoContext = _context.Books.Include(b => b.Store)
                .Skip(id * _recordsPerPage)  //Offset SQL
                .Take(_recordsPerPage); */        

            return View(books);

        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Store)
                .FirstOrDefaultAsync(m => m.Isbn == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}