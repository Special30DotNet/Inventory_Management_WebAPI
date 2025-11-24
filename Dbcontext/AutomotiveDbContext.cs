

using Inventory_Management_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI
{
    public class AutomotiveDbContext:DbContext
    {
        public AutomotiveDbContext(DbContextOptions<AutomotiveDbContext> options):base(options) 
        {
            
        }
    public DbSet<AutomotiveProduct> tblProductMasters {  get; set; }
     
    }
}
