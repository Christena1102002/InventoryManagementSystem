using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Products { get; }
        IRepository<InventoryTransaction> InventoryTransactions { get; }

        Task<int> CompleteAsync();
    }
}
