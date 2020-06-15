using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerShop.Domain;
using ComputerShop.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using ComputerShop.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ComputerShop.Domain.Helper;

namespace ComputerShop.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerOrdersController : Controller
    {
        private readonly ComputerShopContext _context;
        public IConfiguration Configuration { get; }
        public CustomerOrdersController(ComputerShopContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        private Customer GetCurrentCustomer()
        {
            var currentCustomer = _context.Customers.Include(c => c.Account).FirstOrDefault(c => c.Email == this.User.Identity.Name);
            if (currentCustomer == null)
            {
                throw new Exception($"No customer with {this.User.Identity.Name}");
            }

            return currentCustomer;
        }

        

        // GET: CustomerOrders
        public async Task<IActionResult> Index()
        {
            var currentCustomer = GetCurrentCustomer();

            var sqlQuery = @"SELECT *
                              FROM OrderItems
                              Inner Join Orders on OrderItems.OrderId=Orders.Id
                              Inner Join Kits on OrderItems.KitId=Kits.Id
                              Inner Join Categories on Categories.Id=Kits.CategoryId";

            var sqlQueryWithParam = @"SELECT *
                              FROM OrderItems
                              Inner Join Orders on OrderItems.OrderId=Orders.Id
                              Inner Join Kits on OrderItems.KitId=Kits.Id
                              Inner Join Categories on Categories.Id=Kits.CategoryId Where Kits.CategoryId = @CategoryId";

            var result = SQLHelper.ExcecuteSQL(Configuration.GetConnectionString("SQLConnection"),
                sqlQuery);


            var resultWithParams = SQLHelper.ExcecuteSQL(Configuration.GetConnectionString("SQLConnection"),
                sqlQueryWithParam,
                new KeyValueSQlParameter("@CategoryId", "F473D3E8-71E6-4A4B-E484-08D80D0F5764"));


            var customerOrders = _context.Orders.Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Kit)
                .ThenInclude(k => k.Category)
                .Where(o => o.CustomerId == currentCustomer.Id);

            var activeOrder = customerOrders.Where(c => c.Status == OrderStatus.Active).Select(o => new OrderDisplay()
            {
                Id = o.Id,
                Customer = o.Customer,
                CustomerId = o.CustomerId,
                OrderItems = o.OrderItems,
                Status = o.Status,
                CreatedDate = o.CreatedDate,
                TotalPrice = o.OrderItems.Sum(o => o.Kit.Price)
            }).FirstOrDefault();

            var historyOrders = customerOrders.Where(c => c.Status != OrderStatus.Active).ToList().Select(o => new OrderDisplay()
            {
                Id = o.Id,
                Customer = o.Customer,
                CustomerId = o.CustomerId,
                OrderItems = o.OrderItems,
                Status = o.Status,
                CreatedDate = o.CreatedDate,
                TotalPrice = o.OrderItems.Sum(i => i.Kit.Price)
            }).ToList();

            var response = new CustomerOrdersResponse()
            {
                ActiveOrder = activeOrder,
                History = historyOrders
            };
            ViewBag.Ammount = GetCurrentCustomer()?.Account?.Amount;
            return View(response);
        }

        public async Task<IActionResult> Pay(Guid id)
        {
            var currentCustomer = GetCurrentCustomer();

            var customerActiveOrder = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Kit)
                .Where(o => o.CustomerId == currentCustomer.Id)
                .FirstOrDefault(o => o.Status == OrderStatus.Active);

            var orderPrice = customerActiveOrder.OrderItems.Sum(o => o.Kit.Price);
            //check account money
            var customerAbleToPay = (decimal)currentCustomer.Account.Amount > orderPrice;
            if (!customerAbleToPay)
            {
                throw new Exception("Not enough money");
            }

            var invoice = new Invoice()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                Order = customerActiveOrder,
                Amount = orderPrice
            };
            currentCustomer.Account.Amount = (double)((decimal)currentCustomer.Account.Amount - orderPrice);

            _context.Invoices.Add(invoice);
            customerActiveOrder.Status = OrderStatus.Paid;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: CustomerOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: CustomerOrders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: CustomerOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,Status,CreatedDate,Id")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.Id = Guid.NewGuid();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: CustomerOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // POST: CustomerOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CustomerId,Status,CreatedDate,Id")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", order.CustomerId);
            return View(order);
        }

        // GET: CustomerOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: CustomerOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
