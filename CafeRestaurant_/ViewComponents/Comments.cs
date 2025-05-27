using CafeRestaurant_.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CafeRestaurant_.ViewComponents
{
    public class Comments : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public Comments(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var comment = _db.Blogs.Where(i => i.Confirm).ToList();
            return View(comment);
        }
    }
}