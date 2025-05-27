using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CafeRestaurant_.Data;
using CafeRestaurant_.Models;

namespace CafeRestaurant_.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InventoryCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InventoryCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/InventoryCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.InventoryCategories.ToListAsync());
        }

        // GET: Admin/InventoryCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryCategory = await _context.InventoryCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }

            return View(inventoryCategory);
        }

        // GET: Admin/InventoryCategory/Create
        public IActionResult Create()
        {
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/InventoryCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] InventoryCategory inventoryCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryCategory);
        }

        // GET: Admin/InventoryCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryCategory = await _context.InventoryCategories.FindAsync(id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }
            return View(inventoryCategory);
        }

        // POST: Admin/InventoryCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] InventoryCategory inventoryCategory)
        {
            if (id != inventoryCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryCategoryExists(inventoryCategory.Id))
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
            return View(inventoryCategory);
        }

        // GET: Admin/InventoryCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryCategory = await _context.InventoryCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }

            return View(inventoryCategory);
        }

        // POST: Admin/InventoryCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventoryCategory = await _context.InventoryCategories.FindAsync(id);
            _context.InventoryCategories.Remove(inventoryCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryCategoryExists(int id)
        {
            return _context.InventoryCategories.Any(e => e.Id == id);
        }
    }
}
