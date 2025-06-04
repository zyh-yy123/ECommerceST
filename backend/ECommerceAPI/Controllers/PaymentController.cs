using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public PaymentController(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<PaymentResponseDTO>> ProcessPayment(int orderId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Status != "PENDING")
            {
                return BadRequest("订单状态不正确");
            }

            // 模拟支付成功
            order.Status = "COMPLETED";
            order.PaymentStatus = "SUCCESS";
            order.PaymentTransactionId = Guid.NewGuid().ToString();
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new PaymentResponseDTO
            {
                Success = true,
                Message = "支付成功",
                TransactionId = order.PaymentTransactionId
            });
        }
    }
} 