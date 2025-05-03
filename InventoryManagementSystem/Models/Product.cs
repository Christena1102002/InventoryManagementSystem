using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.Models
{
    public class Product 
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int LowStockThreshold { get; set; }

 
        public ICollection<InventoryTransaction> Transactions { get; set; }
    }
}
