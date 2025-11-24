using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgriCultureController : ControllerBase
    {

        private readonly AgriCultureDbContexts _agriCultureDbContexts;
        public AgriCultureController(AgriCultureDbContexts agriCultureDbContext)
        {
            _agriCultureDbContexts = agriCultureDbContext;

        }
        // GET: api/<AgriCultureController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgriCultureProduct>>> GetProducts()
        {
            if (_agriCultureDbContexts.tblProductMasters == null)
                return NotFound("data not found");
            else
                return await _agriCultureDbContexts.tblProductMasters.ToListAsync();
        }


        // GET api/<AgriCultureController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AgriCultureProduct>>> GetProductsById(int id)
        {
            var products = await _agriCultureDbContexts.tblProductMasters.FindAsync(id);

            if (products == null)
            {
                return NotFound("No products found.");
            }
            else
            {
                return Ok(products);
            }
        }

        // POST api/<AgriCultureController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AgriCultureProduct>>> PostProductModel(AgriCultureProduct product)
        {
            _agriCultureDbContexts.tblProductMasters.Add(product);
            await _agriCultureDbContexts.SaveChangesAsync();

            return CreatedAtAction("GetProductsById", new { id = product.id }, product);

        }

        // PUT api/<AgriCultureController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AgriCultureProduct>> PutProductModel(int id, [FromBody] AgriCultureProduct productModel)
        {
            productModel.id = id;  // Force the ID from URL

            _agriCultureDbContexts.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _agriCultureDbContexts.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_agriCultureDbContexts.tblProductMasters.Any(p => p.id == id))
                    return NotFound();
                throw;
            }

            return Ok(productModel);
        }


        // DELETE api/<AgriCultureController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            var product = await _agriCultureDbContexts.tblProductMasters.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            _agriCultureDbContexts.tblProductMasters.Remove(product);
            await _agriCultureDbContexts.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully", id });
        }

    }

}
