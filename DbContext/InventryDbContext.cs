using Inventory_Management_WebAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace Inventory_Management_WebAPI
{
    /*
     * What is DbContext?
        A class that manages:
         = Database connection
         = Maps tables using DbSets
         = Runs queries using EF Core
     */
    public class InventryDbContext : DbContext
    {
        public InventryDbContext(DbContextOptions<InventryDbContext> options) : base(options) { }

        public DbSet<TataMotorsCarsModel> tblProductMasters {  get; set; } // DbSet : to map the table
    }
}
