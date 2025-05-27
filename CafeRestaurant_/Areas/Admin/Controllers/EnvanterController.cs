using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CafeRestaurant_.Data;
using CafeRestaurant_.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CafeRestaurant_.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EnvanterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _he;

        public EnvanterController(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Envanters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Envanters.Include(e => e.InventoryCategory);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Envanters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanters
                .Include(e => e.InventoryCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envanter == null)
            {
                return NotFound();
            }

            return View(envanter);
        }

        // GET: Admin/Envanters/Create
        public IActionResult Create()
        {
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategories, "Id", "Name");
            return View();
        }

        // POST: Admin/Envanters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Envanter envanter)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\Envanter");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (envanter.Image != null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, envanter.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    envanter.Image = @"\Site\Envanter\" + fileName + ext;
                }
                _context.Add(envanter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(envanter);
        }

        // GET: Admin/Envanters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanters.FindAsync(id);
            if (envanter == null)
            {
                return NotFound();
            }
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategories, "Id", "Name", envanter.InventoryCategoryId);
            return View(envanter);
        }

        // POST: Admin/Envanters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Image,Price,InventoryCategoryId,Piece")] Envanter envanter)
        {
            if (id != envanter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(envanter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvanterExists(envanter.Id))
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
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategories, "Id", "Name", envanter.InventoryCategoryId);
            return View(envanter);
        }

        // GET: Admin/Envanters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var envanter = await _context.Envanters
                .Include(e => e.InventoryCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (envanter == null)
            {
                return NotFound();
            }

            return View(envanter);
        }

        // POST: Admin/Envanters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var envanter = await _context.Envanters.FindAsync(id);
            _context.Envanters.Remove(envanter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvanterExists(int id)
        {
            return _context.Envanters.Any(e => e.Id == id);
        }
    }
}
