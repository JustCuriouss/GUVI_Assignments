namespace SecureOrderManagementApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalAmount { get; set; }
        public string? UserName { get; set; }
    }


}
