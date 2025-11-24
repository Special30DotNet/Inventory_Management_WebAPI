using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class AutomotiveProductController : ControllerBase
    {
        

        private readonly AutomotiveDbContext _automotiveDbContext;
        public AutomotiveProductController(AutomotiveDbContext automotiveDbContext)
        {
            _automotiveDbContext = automotiveDbContext;


        }
        // GET: api/<AutomotiveProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutomotiveProduct>>> GetProducts()
        {
            if (_automotiveDbContext.tblProductMasters == null)
                return NotFound("data not found");
            else
                return await _automotiveDbContext.tblProductMasters.ToListAsync();
        }


        // GET api/<AutomotiveProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AutomotiveProduct>>> GetProductsById(int id)
        {
            var products = await _automotiveDbContext.tblProductMasters.FindAsync(id);

            if (products == null)
            {
                return NotFound("No products found.");
            }
            else
            {
                return Ok(products);
            }
        }

        // POST api/<AutomotiveProductController>
        [HttpPost]
        public async Task<ActionResult<IEnumerable<AutomotiveProduct>>> PostProductModel(AutomotiveProduct product)
        {
            _automotiveDbContext.tblProductMasters.Add(product);
            await _automotiveDbContext.SaveChangesAsync();

            return CreatedAtAction("GetProductsById", new { id = product.id }, product);

        }

        // PUT api/<AutomotiveProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<AutomotiveProduct>> PutProductModel(int id, [FromBody] AutomotiveProduct productModel)
        {
            if (id != productModel.id)
                return BadRequest("Product ID mismatch");

            _automotiveDbContext.Entry(productModel).State = EntityState.Modified;

            try
            {
                await _automotiveDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // if record not found
                var exists = _automotiveDbContext.tblProductMasters.Any(p => p.id == id);
                if (!exists)
                    return NotFound();

                throw;  // other DB errors
            }

            return Ok(productModel);  // or return NoContent();
        }


        // DELETE api/<AutomotiveProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductModel(int id)
        {
            var product = await _automotiveDbContext.tblProductMasters.FindAsync(id);

            if (product == null)
                return NotFound("Product not found");

            _automotiveDbContext.tblProductMasters.Remove(product);
            await _automotiveDbContext.SaveChangesAsync();

            return Ok(new { message = "Product deleted successfully", id });
        }

    }


}
