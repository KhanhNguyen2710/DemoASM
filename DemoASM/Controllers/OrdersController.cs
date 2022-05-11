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
using System.Security.Claims;

namespace DemoASM.Controllers
{
    public class OrdersController : Controller
    {
        private readonly DemoASMContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrdersController(DemoASMContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Orders
        public async Task<IActionResult> CheckOut()
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Cart> myCartDetails = await _context.Carts
                .Where(c => c.UserId == currentId)
                .Include(c => c.IsbnNavigation).ToListAsync();
            return View(myCartDetails);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Buy(int? id)
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Cart> myCartDetails = await _context.Carts
                .Where(c => c.UserId == currentId)
                .Include(c => c.IsbnNavigation)
                .ToListAsync();
            using (var transaction = _context.Database.BeginTransaction())
<<<<<<< HEAD
=======
            {
                try
                {
                    //Step 1: create an order
                    Order myOrder = new Order();
                    myOrder.UserId = currentId;
                    myOrder.OrderDate = DateTime.Now;
                    double? total = 0;
                    foreach (Cart cart in myCartDetails)
                    {
                        total += cart.IsbnNavigation.Price * cart.Quantity;
                    }
                    myOrder.TotalPrice = total;
                    _context.Add(myOrder);
                    await _context.SaveChangesAsync();

                    //Step 2: insert all order details by var "myDetailsInCart"
                    foreach (var item in myCartDetails)
                    {
                        OrderDetail detail = new OrderDetail()
                        {
                            OrderId = myOrder.OrderId,
                            Isbn = item.Isbn,
                            Quantity = item.Quantity,
                        };
                        _context.Add(detail);
                    }
                    await _context.SaveChangesAsync();

                    //Step 3: empty/delete the cart we just done for thisUser
                    _context.Carts.RemoveRange(myCartDetails);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error occurred in Checkout" + ex);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,UserId,TotalPrice,Quantity")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.AspNetUsers, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,UserId,TotalPrice,Quantity")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
>>>>>>> adab3d1ad6ce5053aad678850ad99e30f6029ff0
            {
                try
                {
                    //Step 1: create an order
                    Order myOrder = new Order();
                    myOrder.UserId = currentId;
                    myOrder.OrderDate = DateTime.Now;
                    double? total = 0;
                    foreach (Cart cart in myCartDetails)
                    {
                        total += cart.IsbnNavigation.Price * cart.Quantity;
                    }
                    myOrder.TotalPrice = total;
                    _context.Add(myOrder);
                    await _context.SaveChangesAsync();

                    //Step 2: insert all order details by var "myDetailsInCart"
                    foreach (var item in myCartDetails)
                    {
                        OrderDetail detail = new OrderDetail()
                        {
                            OrderId = myOrder.OrderId,
                            Isbn = item.Isbn,
                            Quantity = item.Quantity,
                          
                        };
                        _context.Add(detail);
                    }
                    await _context.SaveChangesAsync();

                    //Step 3: empty/delete the cart we just done for thisUser
                    _context.Carts.RemoveRange(myCartDetails);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (DbUpdateException ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error occurred in Checkout" + ex);
                }
            }
            return RedirectToAction("OrderHistory", "Orders");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> OrderHistory(int? id)
        {
            var currentId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Order> myOrderHistory = await _context.Orders
                .Where(c => c.UserId == currentId)
                .Include(c => c.OrderDetails)
                .ToListAsync();
            return View(myOrderHistory);

        }

          public async Task<IActionResult> ViewOrder()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<Order> myOrder = await _context.Orders
                .Include(od => od.OrderDetails)
               .Where(c => c.UserId == userId)     
               .ToListAsync();

            return View(myOrder);
        }

      /*  private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }*/
    }
}
