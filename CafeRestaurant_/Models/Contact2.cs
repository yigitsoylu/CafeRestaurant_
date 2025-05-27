using System.ComponentModel.DataAnnotations;

namespace CafeRestaurant_.Models
{
    public class Contact2
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
