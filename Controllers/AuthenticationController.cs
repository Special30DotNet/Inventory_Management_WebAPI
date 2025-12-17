using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly InventoryDbContext _context;
        
        public AuthenticationController(InventoryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<RegisterUserModel>> RegisterNewUser(RegisterUserModel registerUser)
        {
            _context.tblUserInformation.Add(registerUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegisterUserModel", new { id = registerUser.Id }, registerUser);
        }
    }
}
