using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public enum TransactionType
    {
        Add,
        Remove,
        Transfer
    }
    public class InventoryTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; } = DateTime.UtcNow;

        [Required]
        public string UserId { get; set; }

        // Navigation Property (لو احتجنا نربطه بالـ User بعدين)
        // public ApplicationUser User { get; set; }

        // (اختياري) لو ضفنا Warehouses بعدين للـ Transfer
        // public int? SourceWarehouseId { get; set; }
        // public int? TargetWarehouseId { get; set; }
        // public Warehouse SourceWarehouse { get; set; }
        // public Warehouse TargetWarehouse { get; set; }
    }
}
