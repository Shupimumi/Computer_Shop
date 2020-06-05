using System;
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
    public class DescriptionsController : Controller
    {
        private readonly ComputerShopContext _context;

        public DescriptionsController(ComputerShopContext context)
        {
            _context = context;
        }

        // GET: Descriptions
        public async Task<IActionResult> Index()
        {
            var allDescriptions = _context.Descriptions.FromSqlRaw("dbo.DescriptionSelectAll").ToList();
            return View(allDescriptions);
        }

        // GET: Descriptions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = _context.Descriptions.FromSqlRaw($"dbo.DescriptionSelect '{id}'").ToList().FirstOrDefault();
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // GET: Descriptions/Create
        public IActionResult Create()
        {
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id");
            return View();
        }

        // POST: Descriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KitId,Name,Value,CreatedDate,Id")] Description description)
        {
            if (ModelState.IsValid)
            {
                description.Id = Guid.NewGuid();
                _context.Add(description);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", description.KitId);
            return View(description);
        }

        // GET: Descriptions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Descriptions.FindAsync(id);
            if (description == null)
            {
                return NotFound();
            }
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", description.KitId);
            return View(description);
        }

        // POST: Descriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("KitId,Name,Value,CreatedDate,Id")] Description description)
        {
            if (id != description.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(description);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DescriptionExists(description.Id))
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
            ViewData["KitId"] = new SelectList(_context.Kits, "Id", "Id", description.KitId);
            return View(description);
        }

        // GET: Descriptions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var description = await _context.Descriptions
                .Include(d => d.Kit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (description == null)
            {
                return NotFound();
            }

            return View(description);
        }

        // POST: Descriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var description = await _context.Descriptions.FindAsync(id);
            _context.Descriptions.Remove(description);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DescriptionExists(Guid id)
        {
            return _context.Descriptions.Any(e => e.Id == id);
        }
    }
}
