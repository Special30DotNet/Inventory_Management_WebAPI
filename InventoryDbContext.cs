using Inventory_Management_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options) { }

        public DbSet<AgricultureModel> tblProductMasters { get; set; }
    }
}
