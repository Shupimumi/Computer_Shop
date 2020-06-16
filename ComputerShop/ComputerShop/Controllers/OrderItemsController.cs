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
using Microsoft.AspNetCore.Identity;
using ComputerShop.Domain.Helper;
using Microsoft.Extensions.Configuration;

namespace ComputerShop.Controllers
{
    [Authorize]
    public class OrderItemsController : Controller
    {
        private readonly ComputerShopContext _context;
        public IConfiguration Configuration { get; }
        public OrderItemsController(ComputerShopContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        // GET: OrderItems
        public async Task<IActionResult> Index()
        {
            var allOrderItems = _context.OrderItems.FromSqlRaw("dbo.OrderItemSelectAll").ToList();
            return View(allOrderItems);
        }

        // GET: OrderItems
        public async Task<IActionResult> Statistics(Guid? categoryId, DateTime? dateFrom, DateTime? dateTo)
        {
            var sqlQuery = @"SELECT OrderItems.Id, OrderItems.CreatedDate , KitId, OrderId
                          FROM OrderItems
                          Inner Join Orders on OrderItems.OrderId=Orders.Id
                          Inner Join Kits on OrderItems.KitId=Kits.Id
                          Inner Join Categories on Categories.Id=Kits.CategoryId
                          Where Kits.CategoryId = IIF(@CategoryId IS NULL, Kits.CategoryId, @CategoryId) 
                            AND OrderItems.CreatedDate >= IIF(@DateFrom IS NULL, OrderItems.CreatedDate, @DateFrom) 
                            AND OrderItems.CreatedDate >= IIF(@DateTo IS NULL, OrderItems.CreatedDate, @DateTo)";
            
            
            var result = SQLHelper.ExcecuteSQL(Configuration.GetConnectionString("SQLConnection"),
                sqlQuery,
                new KeyValueSQlParameter("@CategoryId", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value), //"F473D3E8-71E6-4A4B-E484-08D80D0F5764"
                new KeyValueSQlParameter("@DateFrom",dateFrom.HasValue? (object)dateFrom.Value : DBNull.Value),
                new KeyValueSQlParameter("@DateTo", dateTo.HasValue? (object)dateFrom.Value : DBNull.Value)
                );

            var allOrderItems = ParserHelper.ParseStatistics(result);
            
            foreach(var statistic in allOrderItems)
            {
                // change to use Storred procedure
                var order = _context.Orders.FirstOrDefault(o => o.Id == statistic.OrderId);
                statistic.Order = order;
                // change to use Storred procedure
                statistic.Kit = _context.Kits.FirstOrDefault(k => k.Id == statistic.KitId);
            }


            //var allOrderItems = _context.OrderItems
            //    .Include(o => o.Kit)
            //    .ThenInclude(o => o.Category).ToList();

            //if (categoryId.HasValue)
            //{
            //    allOrderItems = allOrderItems.Where(o => o.Kit.CategoryId == categoryId.Value).ToList();
            //}
            //if (dateFrom.HasValue)
            //{
            //    allOrderItems = allOrderItems.Where(o => o.CreatedDate >= dateFrom).ToList();
            //}
            //if (dateTo.HasValue)
            //{
            //    allOrderItems = allOrderItems.Where(o => o.CreatedDate >= dateTo).ToList();
            //}

            ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
            return View(allOrderItems);
        }

        // GET: OrderItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = _context.OrderItems.FromSqlRaw($"dbo.OrderItemSelect '{id}'").ToList().FirstOrDefault();
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // GET: OrderItems/Create
        public IActionResult Create()
        {
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id");
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id");
            return View();
        }

        // POST: OrderItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitId,OrderId,Quantity,CreatedDate,Id")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                orderItem.Id = Guid.NewGuid();
                _context.Add(orderItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", orderItem.KitId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        // GET: OrderItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", orderItem.KitId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("KitId,OrderId,Quantity,CreatedDate,Id")] OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderItemExists(orderItem.Id))
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
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", orderItem.KitId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderItem = await _context.OrderItems
                .Include(o => o.Kit)
                .Include(o => o.Order)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();

            if (User.IsInRole("Customer"))
            {
                return RedirectToAction("Index", "CustomerOrders");
            }

            return RedirectToAction(nameof(Index));
        }

        private bool OrderItemExists(Guid id)
        {
            return _context.OrderItems.Any(e => e.Id == id);
        }
    }
}
