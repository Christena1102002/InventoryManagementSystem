using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryManagementSystem.data;
using InventoryManagementSystem.DTOs;
using InventoryManagementSystem.Reposatories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Reposatories.implementation
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public ReportsRepository(InventoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LowStockReportDto>> GetLowStockReportAsync()
        {
            return await _context.Products
                .Where(p => p.Quantity <= p.LowStockThreshold)
                .Select(p => new LowStockReportDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    QuantityInStock = p.Quantity,
                    LowStockThreshold = p.LowStockThreshold
                }).ToListAsync();
        }

        public async Task<IEnumerable<TransactionHistoryReportDto>> GetTransactionHistoryAsync(int? productId, DateTime? startDate, DateTime? endDate, string? transactionType)
        {
            var query = _context.InventoryTransactions.AsQueryable();

            if (productId.HasValue)
            {
                query = query.Where(t => t.ProductId == productId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(t => t.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(t => t.Date <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(transactionType))
            {
                query = query.Where(t => t.TransactionType.ToString() == transactionType);
            }

            var result = await query
     .ProjectTo<TransactionHistoryReportDto>(_mapper.ConfigurationProvider)
     .ToListAsync();

            return result;


        }
    }
}
