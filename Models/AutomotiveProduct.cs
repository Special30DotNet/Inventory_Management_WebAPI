using System.ComponentModel.DataAnnotations;

namespace Inventory_Management_WebAPI.Models
{
    public class AutomotiveProduct
    {
        [Key]
        public int id { get; set; }
        public string product_name { get; set; }
        public decimal purchase_price { get; set; }
        public string? product_decription { get; set; }
        public decimal product_selling_price { get; set; }
        public int product_stock_quantity { get; set; }
        public string status { get; set; }
        public DateTime created_at { get; set; }
        public string created_by { get; set; }
        public DateTime? modified_at { get; set; }    
        public string? modified_by { get; set; }
    }
}
