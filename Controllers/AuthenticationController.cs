using System.Threading.Tasks;
using Inventory_Management_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
namespace Inventory_Management_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AgriCultureDbContexts _agriCultureDbContexts;

        public AuthenticationController(AgriCultureDbContexts agriCultureDbContexts)
        {
            _agriCultureDbContexts = agriCultureDbContexts;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<RegisterUser>>> PostRegister(RegisterUser registerUser)
        {

            //Check registerUser's Usename is already exists in DB or not, if already there then message duplicate username
            if (await IsUserNameAvilable(registerUser))
            {
                return Ok("User Already Exist");
            }
            else 
            {
                _agriCultureDbContexts.tblUserInformation.Add(registerUser);
                await _agriCultureDbContexts.SaveChangesAsync();
                return CreatedAtAction("postRegisterData", new { id = registerUser.Id }, registerUser);

            }
               

        }
        private async Task<bool> IsUserNameAvilable(RegisterUser reg)
        {
             
            return  await _agriCultureDbContexts.tblUserInformation.FindAsync(reg.UserName)== null ? false : true;
            
        }
    }
}


