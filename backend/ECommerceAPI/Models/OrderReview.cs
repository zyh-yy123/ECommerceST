using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models;

public class OrderReview
{
    public int Id { get; set; }
    
    [Required]
    public int OrderId { get; set; }
    public Order? Order { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    
    [StringLength(500)]
    public string Comment { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public OrderReview()
    {
        CreatedAt = DateTime.UtcNow;
        Comment = string.Empty;
    }
}