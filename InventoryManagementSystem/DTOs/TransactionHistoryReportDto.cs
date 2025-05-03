namespace InventoryManagementSystem.DTOs
{
    public class TransactionHistoryReportDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int? SourceWarehouseId { get; set; }
        public int? TargetWarehouseId { get;set; }
    }
}
