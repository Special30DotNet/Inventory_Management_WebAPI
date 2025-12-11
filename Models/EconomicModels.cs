//using System.ComponentModel.DataAnnotations;

//namespace Inventory_Management_WebAPI.Models
//{
//    public class EconomicModels
//    {
        using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_WebAPI.Models
    {
        public class EconomicModels
        {
            [Key]
            public int id { get; set; }
            [Required]
            public string product_name { get; set; }
            [Required]
            public decimal purchase_price { get; set; }
            public string? product_decription { get; set; }
            [Required]
            public decimal product_selling_price { get; set; }
            [Required]
            public int product_stock_quantity { get; set; }
            [Required]
            public string status { get; set; }
            [Required]
            public DateTime created_at { get; set; }
            public string? created_by { get; set; }
            public DateTime? modified_at { get; set; }
            public string? modified_by { get; set; }
        }
    }
//}
//}
