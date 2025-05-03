using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class WareHouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ICollection<ProductWareHouseStock> ProductStocks { get; set; }
    }
}
