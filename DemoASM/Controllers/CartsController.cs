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
  /*  [Authorize(Roles = "Customer")]*/
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
        /*public async Task<IActionResult> Index()
        {
            var demoASMContext = _context.Carts.Include(c => c.IsbnNavigation).Include(c => c.User);
            return View(await demoASMContext.ToListAsync());
        }*/

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Carts
//
                .Include(i => i.IsbnNavigation) //
                .Include(i => i.IsbnNavigation)

                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        public IActionResult Cart()
<<<<<<< HEAD
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = _context.Carts.Where(u => u.UserId == currentId).Include(c => c.IsbnNavigation).ToList();
            return View(carts);
        }


        public IActionResult AddToCart (string isbn)
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
=======
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var carts = _context.Carts.Where(c => c.UserId == currentId).Include(c => c.IsbnNavigation).ToList();
            return View(carts);
        }


        public IActionResult AddToCart(string isbn)
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
            var book = _context.Books
                .Where(p => p.Isbn == isbn)
                .FirstOrDefault();
            if (book == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCart();
            var cartitem = cart.Find(p => p.Isbn == isbn);
<<<<<<< HEAD
            if (cartitem != null) // Tăng số lượng khi sản phẩm >= 1
            {
                cartitem.Quantity++; 
=======
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
                _context.Update(cartitem);
                _context.SaveChanges();
            }
            else // sản phẩm mới vào giỏ
            {
                Cart newCartItem = new Cart()
                {
                    UserId = currentId,
                    Isbn = isbn,
                    Quantity = 1 // Số lượng cố định
                };
                SaveCart(newCartItem);
            }
            return RedirectToAction(nameof(Cart));

        }

<<<<<<< HEAD

//.................... Điều chỉnh tăng số lượng sản phẩm..............................................

=======
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0

        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] string isbn, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCart();
            var cartitem = cart.Find(p => p.Isbn == isbn);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.Quantity++;
                _context.Update(cartitem);
                _context.SaveChanges();
            }
            return Ok();
        }

<<<<<<< HEAD
 //...................... Điều chỉnh giảm số lượng sản phẩm..............................................


=======
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
        [Route("/minuscart", Name = "minuscart")]
        [HttpPost]
        public IActionResult minusCart([FromForm] string isbn, [FromForm] int quantity)
        {
<<<<<<< HEAD
            var cart = GetCart();
            var cartitem = cart.Find(p => p.Isbn == isbn); 
            if (cartitem != null)
            {
                // Đã tồn tại, giảm thêm 1
=======
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCart();
            var cartitem = cart.Find(p => p.Isbn == isbn);
            if (cartitem != null)
            {
                // Đã tồn tại, giảm 1 sản phẩm
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
                cartitem.Quantity--;
                _context.Update(cartitem);
                _context.SaveChanges();
            }
            return Ok();
        }

<<<<<<< HEAD
//.....................Xoá sách.......................................................
=======

>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
        public IActionResult RemoveCart(string isbn)
        {
            var cart = GetCart();
            var cartitem = cart.Find(p => p.IsbnNavigation.Isbn == isbn);
            if (cartitem != null)
            {
                _context.Carts.Remove(cartitem);
                _context.SaveChanges();
            }

            //SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

<<<<<<< HEAD
   
=======


>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
        List<Cart> GetCart()
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Cart> carts = _context.Carts.Where(c => c.UserId == currentId).Include(c => c.IsbnNavigation).ToList();
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