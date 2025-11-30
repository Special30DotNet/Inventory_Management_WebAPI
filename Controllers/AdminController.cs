using Inventory_Management_WebAPI.DTO;
using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private InventryDbContext _dbContext;

        public AdminController(InventryDbContext dbContext)
        {
            {
                _dbContext = dbContext;
            }
        }

        [HttpPost("adminLogin")]
        public async Task<IActionResult> AdminLogin( LoginRequest login)
        {
            // Check user in database
            var user = await _dbContext.tblAdminMasters
                .FirstOrDefaultAsync(u => u.user_name == login.userName && u.password == login.password);
            if (login == null) 
            {
                return BadRequest("Enter credentails");
            }

            if (user == null)
            {
                return Unauthorized("Unauthorized user");
            }

            return Ok(new { IsUserVerified = true, Message = "Login successful." });
        }

    }
}
