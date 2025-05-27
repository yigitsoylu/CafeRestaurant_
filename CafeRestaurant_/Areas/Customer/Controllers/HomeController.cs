using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CafeRestaurant_.Models;
using CafeRestaurant_.Data;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CafeRestaurant_.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IToastNotification _toast;
        private readonly IWebHostEnvironment _he;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db, IToastNotification toast, IWebHostEnvironment he)
        {
            _logger = logger;
            _db = db;
            _toast = toast;
            _he=he;
        }

        public IActionResult Index()
        {
            var menu=_db.Menus.Where(i=>i.Special).ToList();
            return View(menu);
        }
        public IActionResult CategoryDetails(int? id)
        {
            var menu=_db.Menus.Where(i=>i.CategoryId ==id).ToList();
            ViewBag.CategoryId = id;
            return View(menu);
        }
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Id,Name,Email,Phone,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.Date = DateTime.Now;
                _db.Add(contact);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Thank you! Your message has been sent successfully.");
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }
        public IActionResult Blog()
        {
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Blog(Blog blog)
        {
            if (ModelState.IsValid)
            {
                blog.Date= DateTime.Now;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_he.WebRootPath, @"Site\menu");
                    var ext = Path.GetExtension(files[0].FileName);
                    if (blog.Image != null)
                    {
                        var imagePath = Path.Combine(_he.WebRootPath, blog.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var filesStreams = new FileStream(Path.Combine(uploads, fileName + ext), FileMode.Create))
                    {
                        files[0].CopyTo(filesStreams);
                    }

                    blog.Image = @"\Site\menu\" + fileName + ext;
                }
                _db.Add(blog);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Thanks for your comment! It will appear on the comments page after approval.");
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }
        public IActionResult About()
        {
            var about = _db.Abouts.ToList();
            return View(about);
        }
        public IActionResult Gallery()
        {
            var gallery = _db.Galleries.ToList();
            return View(gallery);
        }
        public IActionResult Reservation()
        {
            return View();
        }

        // POST: Admin/Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reservation([Bind("Id,Name,Email,Phone,People,Time,Date")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _db.Add(reservation);
                await _db.SaveChangesAsync();
                _toast.AddSuccessToastMessage("Reservation completed successfully!");
                return RedirectToAction(nameof(Index));
            }
            return View(reservation);
        } 
        public IActionResult Menu()
        {
            var menu = _db.Menus.ToList();
            return View(menu);
        }

       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
