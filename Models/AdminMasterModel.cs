using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_WebAPI.Models
{
    public class AdminMasterModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string user_name { get; set; }

        [Required]
        public string password { get; set; }
    }
}
