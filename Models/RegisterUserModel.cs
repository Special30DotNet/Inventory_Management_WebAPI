using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_WebAPI.Models
{
    public class RegisterUserModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

    }
}
