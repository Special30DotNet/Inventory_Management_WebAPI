//using Inventory_Management_WebAPI.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace Inventory_Management_WebAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AgricultureController : ControllerBase
//    {
//        private InventoryDbContext _inventoryDbContext;
//        public AgricultureController(InventoryDbContext inventoryDbContext)
//        {
//            _inventoryDbContext = inventoryDbContext;
//        }

//        // GET: api/<ProductsController>
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<AgricultureModel>>> GetProducts()
//        {
//            if (_inventoryDbContext.tblProductMasters == null)
//                return NotFound();
//            else
//                return await _inventoryDbContext.tblProductMasters.ToListAsync();
//        }

//        // GET api/<ProductsController>/5
//        [HttpGet("{id}")]
//        public string Get(int id)
//        {
//            return "value";
//        }

//        // POST api/<ProductsController>
//        [HttpPost]
//        public void Post([FromBody] string value)
//        {
//        }

//        // PUT api/<ProductsController>/5
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] string value)
//        {
//        }

//        // DELETE api/<ProductsController>/5
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//        }
//    }
//}



using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgricultureController : ControllerBase
    {
        private readonly InventoryDbContext _inventoryDbContext;

        public AgricultureController(InventoryDbContext inventoryDbContext)
        {
            _inventoryDbContext = inventoryDbContext;
        }

        // ------------------------------------------------------------
        // 1️⃣ GET ALL PRODUCTS
        // ------------------------------------------------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AgricultureModel>>> GetProducts()
        {
            if (_inventoryDbContext.tblProductMasters == null)
                return NotFound();

            return await _inventoryDbContext.tblProductMasters.ToListAsync();
        }

        // ------------------------------------------------------------
        // 2️⃣ GET PRODUCT BY ID
        // ------------------------------------------------------------
        [HttpGet("{id}")]
        public async Task<ActionResult<AgricultureModel>> GetProductById(int id)
        {
            var product = await _inventoryDbContext.tblProductMasters.FindAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found");

            return product;
        }

        // ------------------------------------------------------------
        // 3️⃣ CREATE PRODUCT (POST)
        // ------------------------------------------------------------
        [HttpPost]
        public async Task<ActionResult<AgricultureModel>> PostProduct([FromBody] AgricultureModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _inventoryDbContext.tblProductMasters.Add(model);
            await _inventoryDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProductById), new { id = model.id }, model);
        }

        // ------------------------------------------------------------
        // 4️⃣ UPDATE PRODUCT (PUT)
        // ------------------------------------------------------------
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromBody] AgricultureModel model)
        {
            if (id != model.id)
                return BadRequest("ID mismatch");

            var existingProduct = await _inventoryDbContext.tblProductMasters.FindAsync(id);

            if (existingProduct == null)
                return NotFound($"Product with ID {id} not found");

            // Update properties
            existingProduct.product_name = model.product_name;
            existingProduct.purchase_price = model.purchase_price;
            existingProduct.product_decription = model.product_decription;

            await _inventoryDbContext.SaveChangesAsync();

            return Ok(existingProduct);
        }

        // ------------------------------------------------------------
        // 5️⃣ DELETE PRODUCT
        // ------------------------------------------------------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var product = await _inventoryDbContext.tblProductMasters.FindAsync(id);

            if (product == null)
                return NotFound($"Product with ID {id} not found");

            _inventoryDbContext.tblProductMasters.Remove(product);
            await _inventoryDbContext.SaveChangesAsync();

            return Ok($"Product with ID {id} deleted successfully");
        }
    }
}
