using InventoryManagementSystem.data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.interfaces;

namespace InventoryManagementSystem.Reposatories.implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly InventoryDbContext _context;
        public ProductRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
