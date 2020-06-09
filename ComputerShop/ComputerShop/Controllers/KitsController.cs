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

namespace ComputerShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class KitsController : Controller
    {
        private readonly ComputerShopContext _context;

        public KitsController(ComputerShopContext context)
        {
            _context = context;
        }

        // GET: Kits
        public async Task<IActionResult> Index()
        {
            var allKits = _context.Kits.FromSqlRaw("dbo.KitSelectAll").ToList();
            return View(allKits);
        }

        // GET: Kits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kit = _context.Kits.FromSqlRaw($"dbo.KitSelect '{id}'").ToList().FirstOrDefault();
            if (kit == null)
            {
                return NotFound();
            }

            return View(kit);
        }

        // GET: Kits/Create
        public IActionResult Create()
        {
            ViewData["Name"] = new SelectList(_context.Categories, "Name", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Kits/Create
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

        // GET: Kits/Edit/5
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

        // POST: Kits/Edit/5
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

        // GET: Kits/Delete/5
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

        // POST: Kits/Delete/5
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
