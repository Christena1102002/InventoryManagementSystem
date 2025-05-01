using InventoryManagementSystem.data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.Reposatories.implementation
{
    public class InventoryTransactionsRepository:GenericRepository<InventoryTransaction> ,IInventoryTransactionIRepository
    {
        private readonly InventoryDbContext _context;
        public InventoryTransactionsRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
