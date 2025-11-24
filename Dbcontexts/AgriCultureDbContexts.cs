using Inventory_Management_WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI
{
    public class AgriCultureDbContexts : DbContext
    {

        public AgriCultureDbContexts(DbContextOptions<AgriCultureDbContexts> options) : base(options)
        {
           
        }
        public DbSet<AgriCultureProduct> tblProductMasters { get; set; }



    }
        
}
