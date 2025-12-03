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
    public class ProductsSellController : ControllerBase
    {
        private readonly InventoryDbContext _context;

        public ProductsSellController(InventoryDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsSell
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GettblProductMasters()
        {
            return await _context.tblProductMasters.ToListAsync();
        }

        // GET: api/ProductsSell/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductModel(int id)
        {
            var productModel = await _context.tblProductMasters.FindAsync(id);

            if (productModel == null)
            {
                return NotFound();
            }

            return productModel;
        }

        // PUT: api/ProductsSell/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductModel(int id, ProductModel productModel)
        {
            if (id != productModel.id)
            {
                return BadRequest();
            }

            _context.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsSell
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductModel(ProductModel productModel)
        {
            _context.tblProductMasters.Add(productModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductModel", new { id = productModel.id }, productModel);
        }

        // DELETE: api/ProductsSell/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            var productModel = await _context.tblProductMasters.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }

            _context.tblProductMasters.Remove(productModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductModelExists(int id)
        {
            return _context.tblProductMasters.Any(e => e.id == id);
        }
    }
}
