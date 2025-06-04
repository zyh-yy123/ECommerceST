using System.ComponentModel.DataAnnotations;

namespace ECommerceAPI.DTOs
{
    public class PaymentRequestDTO
    {
        [Required]
        public string PaymentMethod { get; set; }
    }

    public class PaymentResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string TransactionId { get; set; }
    }

    public class OrderPaymentDTO
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
    }
} 