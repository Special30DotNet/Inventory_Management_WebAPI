using Microsoft.AspNetCore.Mvc;
using Inventory_Management_WebAPI.Models;

using Microsoft.EntityFrameworkCore;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Inventory_Management_WebAPI.Controllers
{
    //Line no 9/10 They define how your controller behaves and how your API routes are generated.
    [Route("api/[controller]")]     //Sets API path using controller name
    [ApiController]                 //Enables automatic validation, binding, and API behaviors
    public class ProductsController : ControllerBase
    {
        private InventoryDbContext _inventoryDbContext;                 //It is a variable that stores your database context.
        public ProductsController(InventoryDbContext inventoryDbContext)        //constructor receives DB context from DI
        {
            _inventoryDbContext = inventoryDbContext;                         //store it so I can use it later”
        }


        // GET: api/<ProductsController>
        [HttpGet]

        /* public => This method can be accessed from outside (API clients like Postman, Angular, etc.)
        async => This method runs asynchronously (non-blocking).
        Helps performance — it does not freeze the thread while waiting for DB response.
        Task<T> => means this method returns something in future (because async).
        ActionResult<T> => means the result can be:
            Data (200 OK)
            Error (404, 500, etc.)
        IEnumerable<TataMotorsCarsModel> => means it returns a list of car/product objects.
        ToListAsync() => Executes SQL query asynchronously and returns data as a List.
        */
        public async Task<ActionResult<IEnumerable<EconomicModels>>> GetProducts()
        {
            var products = await _inventoryDbContext.tblProductMasters.ToListAsync();
            if (products == null)
                return NotFound("No products found");
            else
                return Ok(products);
        }

        // Get Product list by id
        [HttpGet("{Id}")]
        /* FindAsync(id) => is an Entity Framework Core method used to quickly find a single record by its primary key.
         * FirstOrDefaultAsync => Searching by any field (slower than findAsync());
         * eg. var product = await _Inverntrydbcontext.tblProductMasters.FirstOrDefaultAsync(p => p.id == id);
         */

        public async Task<ActionResult<IEnumerable<EconomicModels>>> GetSingleProduct(int id)
        {
            var product = await _inventoryDbContext.tblProductMasters.FindAsync(id);
            //var product = await _Inverntrydbcontext.tblProductMasters.FirstOrDefaultAsync(p => p.product_name == name);
            if (product == null)
            {
                return NotFound("NO Product Found");

            }
            return Ok("Product");

        }

        // Create product record
        //[HttpPost]
         [HttpPost("AddProduct")]
        public async Task<ActionResult<EconomicModels>> AddProduct(EconomicModels economicModels)
        {
            if (economicModels == null)
            {
                return BadRequest("Bad Request");
            }
            // Set default created_at if not provided
            if (economicModels.created_at == null || economicModels.created_at < new DateTime(1753, 1, 1))
            {
                economicModels.created_at = DateTime.Now;
            }

            // Set modified_at as null initially
            economicModels.modified_at = null;
            _inventoryDbContext.tblProductMasters.Add(economicModels);
            await _inventoryDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), new { id = economicModels.id }, economicModels);
        }


        //Delet product record
        [HttpDelete("DeletRecord/{id}")]
        public async Task<IActionResult>DeletRecord(int id)
        {
            var product = await _inventoryDbContext.tblProductMasters.FindAsync(id);
            if(product == null)
            {
                return NotFound("Record not found");
            }
            _inventoryDbContext.tblProductMasters.Remove(product);
            await _inventoryDbContext.SaveChangesAsync();
            return Ok("Record deleted");

        }




        [HttpPut("UpdateRecord/{id}")]
        
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] EconomicModels models)
        {
            
            if (models == null)
                return BadRequest("Invalid data");

            var existingProduct = await _inventoryDbContext.tblProductMasters.FindAsync(id);

            if (existingProduct == null)
                return NotFound("Product not found");

            // Update fields
            existingProduct.product_name = models.product_name;
            existingProduct.product_decription = models.product_decription;
            existingProduct.product_selling_price = models.product_selling_price;
            existingProduct.product_stock_quantity = models.product_stock_quantity;
            existingProduct.status = models.status;

            // Save changes
            await _inventoryDbContext.SaveChangesAsync();
            //return Ok("Product updated successfully");
            return Ok(new { success = true, message = "Product updated successfully" });
        }
    }
}
