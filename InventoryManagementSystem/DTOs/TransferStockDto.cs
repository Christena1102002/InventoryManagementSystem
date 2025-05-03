namespace InventoryManagementSystem.DTOs
{
    public class TransferStockDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int SourceWarehouseId { get; set; }
        public int TargetWarehouseId { get; set; }
    }
}
