using InventoryManagementSystem.DTOs;

namespace InventoryManagementSystem.Reposatories.interfaces
{
    public interface IReportsRepository
    {
        Task<IEnumerable<LowStockReportDto>> GetLowStockReportAsync();
        Task<IEnumerable<TransactionHistoryReportDto>> GetTransactionHistoryAsync(int? productId, DateTime? startDate, DateTime? endDate, string? transactionType);
    }
}
