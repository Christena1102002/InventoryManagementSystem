using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




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
        [ForeignKey(nameof(Product))]
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
        // public ApplicationUser User { get; set; }  // إذا رغبتي تربطيها بالمستخدم

        //––– نقل المخزون بين مخازن –––//

        // المفتاح الخارجي للمخزن المصدر
        [ForeignKey(nameof(SourceWarehouse))]
        public int? SourceWarehouseId { get; set; }
        public WareHouse SourceWarehouse { get; set; }

        // المفتاح الخارجي للمخزن الوجهة
        [ForeignKey(nameof(TargetWarehouse))]
        public int? TargetWarehouseId { get; set; }
        public WareHouse TargetWarehouse { get; set; }
    }
}
