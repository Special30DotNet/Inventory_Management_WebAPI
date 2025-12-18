using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory_Management_WebAPI.Models
{
    public class RegisterUser
    {
        //public int Id { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }
        //public string Role {  get; set; }
        //public string FirstName { get; set; }

        //public string LastName { get; set; }


       
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Key]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

    }
}
