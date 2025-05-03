using InventoryManagementSystem.data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Reposatories.implementation
{
    public class ProductWareHouseStockRepository : GenericRepository<ProductWareHouseStock>, IProductWarehouseStockRepository
    {
        private readonly InventoryDbContext _context;
        public ProductWareHouseStockRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }
      

        public async Task<ProductWareHouseStock> GetByProductAndWarehouseAsync(int productId, int warehouseId)
        {
            return await _context.ProductWareHouseStocks
                 .FirstOrDefaultAsync(s => s.ProductId == productId 
                 && s.WareHouseId == warehouseId);
        }
        public void Add(ProductWareHouseStock stock)
        {
            _context.ProductWareHouseStocks.Add(stock);
        }

        public void Update(ProductWareHouseStock stock)
        {
            _context.ProductWareHouseStocks.Update(stock);
        }

    }
    }
