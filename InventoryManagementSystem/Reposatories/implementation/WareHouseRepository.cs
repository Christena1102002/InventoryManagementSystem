using InventoryManagementSystem.data;

using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Reposatories.interfaces;
using Microsoft.EntityFrameworkCore;
namespace InventoryManagementSystem.Reposatories.implementation
{
    public class WareHouseRepository: GenericRepository<WareHouse>, IWareHouseRepository
    {
    
        private readonly InventoryDbContext _context;
        public WareHouseRepository(InventoryDbContext context) : base(context)
        {
            _context = context;
        }

      
    }
}
