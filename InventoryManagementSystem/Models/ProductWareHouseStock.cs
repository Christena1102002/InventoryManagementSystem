using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagementSystem.Models
{
    public class ProductWareHouseStock
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [ForeignKey(nameof(WareHouse))]
        public int WareHouseId { get; set; }
        public WareHouse WareHouse { get; set; }

      
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}