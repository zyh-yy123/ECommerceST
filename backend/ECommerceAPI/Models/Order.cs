namespace ECommerceAPI.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string ShippingInfo { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public int UserId { get; set; }

    public User? User { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
