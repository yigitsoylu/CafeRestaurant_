using System.ComponentModel.DataAnnotations;

namespace CafeRestaurant_.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
