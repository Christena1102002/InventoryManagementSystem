namespace InventoryManagementSystem.DTOs
{
    public class LowStockReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public int LowStockThreshold { get; set; }
    }
}
