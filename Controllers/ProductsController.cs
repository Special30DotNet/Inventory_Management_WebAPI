using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

        // add product in record
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
            return Ok("Product saved!");
        }

        // Delete product Api
        [HttpDelete("DeleteRecord/{Id}")]
        public async Task<IActionResult> DeleteRecord(int Id)
        {
            var Product = await _Inverntrydbcontext.tblProductMasters.FindAsync(Id);
            if (Product == null)
            {
                return NotFound("Product not found");
            }
            _Inverntrydbcontext.tblProductMasters.Remove(Product);
            await _Inverntrydbcontext.SaveChangesAsync();
            return Ok("Record deleted!");
        }

        // Update record using put method
        [HttpPut("EditProduct/{Id}")]
        public async Task<IActionResult> EditRecord(int Id, ProductMasterModel productMasterModel)
        {
            var Exisstingproduct = await _Inverntrydbcontext.tblProductMasters.FindAsync(Id);
            if (Id != productMasterModel.id)
            {
                return BadRequest("Bad request");
            }
            if (Exisstingproduct == null)
            {
                return NotFound("Product not found");
            }
            if (productMasterModel != null) {
                Exisstingproduct.product_name = productMasterModel.product_name;
                Exisstingproduct.product_decription = productMasterModel.product_decription;
                Exisstingproduct.purchase_price = productMasterModel.purchase_price;
                Exisstingproduct.product_selling_price = productMasterModel.product_selling_price;
                Exisstingproduct.product_stock_quantity = productMasterModel.product_stock_quantity;
                Exisstingproduct.status = productMasterModel.status;
                Exisstingproduct.image_url = productMasterModel.image_url;
                Exisstingproduct.created_at = productMasterModel.created_at;
                Exisstingproduct.created_by = productMasterModel.created_by;
                Exisstingproduct.modified_at = productMasterModel.modified_at;
                Exisstingproduct.modified_by = productMasterModel.modified_by;
                //_Inverntrydbcontext.Entry(productMasterModel).State = EntityState.Modified; // This is useful when we have less fields
            }
            await _Inverntrydbcontext.SaveChangesAsync();
            return Ok("Product updated successfully");
        }
    }
}
