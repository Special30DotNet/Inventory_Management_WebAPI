namespace Inventory_Management_WebAPI.Models
{
    public class clothesInventory
    {
        public int Id { get; set; }
        public string Product_Name { get; set; }
        public decimal Purchase_Price { get; set; }
        public string Product_Decription { get; set; }
        public decimal Product_Selling_Price { get; set; }
        public int Product_Stock_Quantity { get; set; }
        public string Status { get; set; }
        public DateTime Created_At { get; set; }
        public string Created_By { get; set; }
        public DateTime? Modified_At { get; set; }
        public string Modified_By { get; set; }
    }
}
