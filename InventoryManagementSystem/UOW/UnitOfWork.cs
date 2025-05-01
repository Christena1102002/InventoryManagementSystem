using InventoryManagementSystem.data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.implementation;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.UOW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly InventoryDbContext _context;
     

        public IRepository<Product> Products { get;}
        public IRepository<InventoryTransaction> InventoryTransactions { get;}

        public UnitOfWork(InventoryDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);

            InventoryTransactions = new InventoryTransactionsRepository(_context);
        }

       
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
