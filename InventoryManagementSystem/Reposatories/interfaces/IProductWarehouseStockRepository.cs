using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Reposatories.interfaces
{
    public interface IProductWarehouseStockRepository :IRepository<ProductWareHouseStock>
    {
        Task<ProductWareHouseStock> GetByProductAndWarehouseAsync(int productId, int warehouseId);

        void Add(ProductWareHouseStock stock);
        void Update(ProductWareHouseStock stock);
    }
}
