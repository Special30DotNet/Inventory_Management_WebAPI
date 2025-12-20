using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    
    public class AuthenticationController : ControllerBase
    {
        private  readonly InventryDbContext _dbContext;

        public AuthenticationController (InventryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<IEnumerable<RegisterUserModel>>> RegisterNewUSer(RegisterUserModel Regmodel)
        {
            //var userDetail = await _dbContext.tblUserInformation.FindAsync(Regmodel.UserName);
            if (Regmodel == null)
            {
                return BadRequest("Bad request");
            }


            if (await IsUserExist(Regmodel))
            {
                return Ok("User already exist");
            }

            if (IsUserValid(Regmodel.UserName))
            {
                _dbContext.tblUserInformation.Add(Regmodel);
                await _dbContext.SaveChangesAsync();
                return Ok("user created");

            }
            else
            {
                return Ok("User is not valid");
            }
        }
        private async Task<bool> IsUserExist(RegisterUserModel reg)
        {
            return await _dbContext.tblUserInformation.AnyAsync(u => u.UserName.ToUpper() == reg.UserName.ToUpper());
        }

        private bool IsUserValid(string UserName)
        {
            // eg. ASWKO6588C pan card pattern 
            string pattern = @"^[A-Z]{5}[0-9]{4}[A-Z]$";
            return Regex.IsMatch(UserName.ToUpper(), pattern.ToUpper());
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult> GetUSer(int id)
        {
            var user = await _dbContext.tblUserInformation.FindAsync(id);
            if (id == null)
            {
                return BadRequest("Incorrect user id");
            }
            if (user == null)
            {
                return BadRequest("User not found");
            } else
            {
                return Ok(new { user });
            }

        }
    }
}
