using System.ComponentModel.DataAnnotations;

namespace CafeRestaurant_.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
    }
}
