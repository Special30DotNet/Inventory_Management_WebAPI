using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inventory_Management_WebAPI.DTO;
using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly InventryDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(InventryDbContext dbContext, IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            {
                _dbContext = dbContext;
                _config = configuration;
                _userManager = userManager;
                _roleManager = roleManager;
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

            string token = GenerateJwtToken(user.user_name);
            //return Ok(new { IsUserVerified = true, Message = "Login successful." });
            return Ok(new { Token = token, Message = "Login successful" });
        }

        private string GenerateJwtToken(string username)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(double.Parse(jwtSettings["DurationInMinutes"])),
            signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
