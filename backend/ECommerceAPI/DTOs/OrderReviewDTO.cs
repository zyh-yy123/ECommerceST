using System;
using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class CreateOrderReviewDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;
    }

    public class OrderReviewDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
} 