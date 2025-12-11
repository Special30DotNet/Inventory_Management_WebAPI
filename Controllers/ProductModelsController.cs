
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Inventory_Management_WebAPI;
using Inventory_Management_WebAPI.Models;
namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductModelsController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        public ProductModelsController(InventoryDbContext context)
        {
            _context = context;
        }
        // GET: api/ProductModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EconomicModels>>> GettblProductMasters()
        {
            return await _context.tblProductMasters.ToListAsync();
        }
    }
}
