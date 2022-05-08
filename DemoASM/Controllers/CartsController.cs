#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DemoASM.Data;
using DemoASM.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DemoASM.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CartsController : Controller
    {
        private readonly DemoASMContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartsController(DemoASMContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }


        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var demoASMContext = _context.Carts.Include(c => c.IsbnNavigation).Include(c => c.User);
            return View(await demoASMContext.ToListAsync());
        }

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
                .Include(c => c.IsbnNavigation)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }
       /* public IActionResult Cart()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = _context.Carts.Where(c => c.UserId == user).Include(c => c.IsbnNavigation).ToList();
            return View(carts);
        }*/
        public IActionResult AddToCart (string isbn)
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var carts = _context.Carts.Where(c => c.UserId == user).Include(c => c.IsbnNavigation).ToList();
            var book = _context.Books
                .Where(p => p.Isbn == isbn)
                .FirstOrDefault();
            if (book == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCart();
            var cartitem = cart.Find(p => p.Isbn == isbn);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
                _context.Update(cartitem);
                _context.SaveChanges();
            }
            else
            {
                Cart newCartItem = new Cart()
                {
                    UserId = user,
                    Isbn = isbn,
                    Quantity = 1
                };
                SaveCart(newCartItem);
            }
            return RedirectToAction(nameof(Cart));
        }
        List<Cart> GetCart()
        {
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Cart> carts = _context.Carts.Where(c => c.UserId == user).Include(c => c.IsbnNavigation).ToList();
            if (carts != null)
            {
                return carts;
            }
            return new List<Cart>();
        }
        void SaveCart(Cart ls)
        {
            _context.Add(ls);
            _context.SaveChanges();

        }

    }
}