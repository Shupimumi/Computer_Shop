﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComputerShop.Domain;
using ComputerShop.Domain.Entities;

namespace ComputerShop.Controllers
{
    public class CustomerKitsController : Controller
    {
        private readonly ComputerShopContext _context;

        public CustomerKitsController(ComputerShopContext context)
        {
            _context = context;
        }

        // GET: CustomerKits
        public async Task<IActionResult> Index(Guid customerId)
        {
            //Doto storred
            var currentCustomer = _context.Customers.FirstOrDefault(c => c.Id == customerId);
            if(currentCustomer == null)
			{
                throw new Exception($"No customer with id {customerId}");
			}

            var computerShopContext = _context.Kits.Include(k => k.Category);
            ViewBag.CustomerId = customerId;
            return View(await computerShopContext.ToListAsync());
        }

        public async Task<IActionResult> PlaceOrder(Guid customerId, Guid kitId)
		{
            var currentCustomer = _context.Customers.FirstOrDefault(c => c.Id == customerId);
            if (currentCustomer == null)
            {
                throw new Exception($"No customer with id {customerId}");
            }
            var selectedKit = _context.Kits.FirstOrDefault(x => x.Id == kitId);
            if (currentCustomer == null)
            {
                throw new Exception($"No kit with id {kitId}");
            }

			var currentOrder = _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefault(x => x.CustomerId == customerId && x.Status == OrderStatus.Active);
            if(currentOrder == null)
			{
                currentOrder = new Order
                {
                    CustomerId = currentCustomer.Id,
                    OrderItems = new List<OrderItem>(),
                    Status = OrderStatus.Active,
     //               Invoice = new Invoice()
					//{
     //                   Amount = 1000,

					//}
                };
			}

            var anyItemInCategory = currentOrder.OrderItems.Any(x => x.Kit.CategoryId == selectedKit.CategoryId);
			if (anyItemInCategory)
			{
                throw new Exception($"Item of category {selectedKit.Category.Name} already selected");
			}

            currentOrder.OrderItems.Add(new OrderItem()
            {
                KitId = selectedKit.Id
            });

            _context.Orders.Add(currentOrder);
            var result = _context.SaveChanges();

            return View();
        }

        // GET: CustomerKits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits
                .Include(k => k.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kit == null)
            {
                return NotFound();
            }

            return View(kit);
        }

        // GET: CustomerKits/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: CustomerKits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,Price,ImageLink,CreatedDate,Id")] Kit kit)
        {
            if (ModelState.IsValid)
            {
                kit.Id = Guid.NewGuid();
                _context.Add(kit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", kit.CategoryId);
            return View(kit);
        }

        // GET: CustomerKits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits.FindAsync(id);
            if (kit == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", kit.CategoryId);
            return View(kit);
        }

        // POST: CustomerKits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,Price,ImageLink,CreatedDate,Id")] Kit kit)
        {
            if (id != kit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KitExists(kit.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", kit.CategoryId);
            return View(kit);
        }

        // GET: CustomerKits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kit = await _context.Kits
                .Include(k => k.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kit == null)
            {
                return NotFound();
            }

            return View(kit);
        }

        // POST: CustomerKits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var kit = await _context.Kits.FindAsync(id);
            _context.Kits.Remove(kit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KitExists(Guid id)
        {
            return _context.Kits.Any(e => e.Id == id);
        }
    }
}
