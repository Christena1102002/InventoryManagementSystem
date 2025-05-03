using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.implementation;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<InventoryTransaction> InventoryTransactions { get; }
        //IRepository<ProductWareHouseStock> ProductWarehouseStocks { get; }
        IProductWarehouseStockRepository ProductWarehouseStocks { get; }
       
        IRepository<WareHouse> WareHouses { get; }
        Task<int> CompleteAsync();
    }
}
