using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CafeRestaurant_.Models
{
    public class InventoryCategory
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public List  <Envanter>? Envanters{ get; set; }

    }
}
