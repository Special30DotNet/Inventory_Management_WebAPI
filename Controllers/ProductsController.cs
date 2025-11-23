using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private InventryDbContext _Inverntrydbcontext;
        public ProductsController(InventryDbContext Inverntrydbcontext)
        {
            _Inverntrydbcontext = Inverntrydbcontext;
        }

        // Get product list
        [HttpGet("Products")]
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
        public async Task<ActionResult<IEnumerable<ProductMasterModel>>> GetProductsList()
        {
            var products = await _Inverntrydbcontext.tblProductMasters.ToListAsync();
            if (products == null)
            {
                return NotFound("No products found");
            }
            return Ok(products);
        }

        // Get Product list by id
        [HttpGet("Products/{Id}")]
        /* FindAsync(id) => is an Entity Framework Core method used to quickly find a single record by its primary key.
         * FirstOrDefaultAsync => Searching by any field (slower than findAsync());
         * eg. var product = await _Inverntrydbcontext.tblProductMasters.FirstOrDefaultAsync(p => p.id == id);
         */
        public async Task<ActionResult<ProductMasterModel>> GetSingleProduct(int Id)
        {
            var product = await _Inverntrydbcontext.tblProductMasters.FindAsync(Id);
            //var product = await _Inverntrydbcontext.tblProductMasters.FirstOrDefaultAsync(p => p.product_name == name);
            if (product == null)
            {
                return NotFound("No product found");
            }
            return Ok(product);
        }

        // Create product record
        [HttpPost("AddProduct")]
        public async Task<ActionResult<ProductMasterModel>> AddProduct(ProductMasterModel productDetails)
        {
            if(productDetails == null)
            {
                return BadRequest("Bad Request");
            }
             _Inverntrydbcontext.tblProductMasters.Add(productDetails);
             await _Inverntrydbcontext.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetProductsList), new { id = productDetails.id },productDetails);
            return Ok("Product saved successfully");
        }
    }
}
