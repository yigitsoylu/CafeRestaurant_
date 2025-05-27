using CafeRestaurant_.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CafeRestaurant_.ViewComponents
{
    public class Contact2 : ViewComponent
    {
        private readonly ApplicationDbContext _db;
        public Contact2(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var contact2 = _db.Contact2s.ToList();
            return View(contact2);
        }
    }
}