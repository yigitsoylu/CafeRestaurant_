using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CafeRestaurant_.Models
{
    public class Envanter
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
       
        public double Price { get; set; }
        public int Piece { get; set; }
        public int InventoryCategoryId { get; set; }
        [ForeignKey("InventoryCategoryId")]
        public InventoryCategory InventoryCategory { get; set; }
    }
}
