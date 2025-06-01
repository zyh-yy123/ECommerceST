namespace ECommerceAPI.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string ThumbnailUrl { get; set; } = string.Empty;

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}

