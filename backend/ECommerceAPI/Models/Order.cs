using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.Models;

public class Order
{
    public int Id { get; set; }
    
    [Required]
    public string OrderNumber { get; set; }
    
    [Required]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public List<OrderItem> OrderItems { get; set; }
    
    [Required]
    public decimal TotalAmount { get; set; }
    
    [Required]
    public string Status { get; set; }
    
    public string PaymentMethod { get; set; }
    
    public string PaymentStatus { get; set; }
    
    public string PaymentTransactionId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime? UpdatedAt { get; set; }
    
    public Order()
    {
        OrderItems = new List<OrderItem>();
        CreatedAt = DateTime.UtcNow;
        Status = "PENDING";
        PaymentStatus = "PENDING";
        OrderNumber = GenerateOrderNumber();
        PaymentMethod = "WECHAT";
        PaymentTransactionId = string.Empty;
    }
    
    private string GenerateOrderNumber()
    {
        return $"ORD{DateTime.UtcNow:yyyyMMddHHmmss}{new Random().Next(1000, 9999)}";
    }
} 