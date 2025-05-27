using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CafeRestaurant_.Data;
using CafeRestaurant_.Models;
using Microsoft.AspNetCore.Authorization;

namespace CafeRestaurant_.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class Contact2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Contact2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Contact2
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contact2s.ToListAsync());
        }

        // GET: Admin/Contact2/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact2 = await _context.Contact2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact2 == null)
            {
                return NotFound();
            }

            return View(contact2);
        }

        // GET: Admin/Contact2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Phone,Address")] Contact2 contact2)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact2);
        }

        // GET: Admin/Contact2/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact2 = await _context.Contact2s.FindAsync(id);
            if (contact2 == null)
            {
                return NotFound();
            }
            return View(contact2);
        }

        // POST: Admin/Contact2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Phone,Address")] Contact2 contact2)
        {
            if (id != contact2.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Contact2Exists(contact2.Id))
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
            return View(contact2);
        }

        // GET: Admin/Contact2/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact2 = await _context.Contact2s
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact2 == null)
            {
                return NotFound();
            }

            return View(contact2);
        }

        // POST: Admin/Contact2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact2 = await _context.Contact2s.FindAsync(id);
            _context.Contact2s.Remove(contact2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Contact2Exists(int id)
        {
            return _context.Contact2s.Any(e => e.Id == id);
        }
    }
}
