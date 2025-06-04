using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.DTOs;
using ECommerceAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderReviewController : ControllerBase
    {
        private readonly ECommerceDbContext _context;

        public OrderReviewController(ECommerceDbContext context)
        {
            _context = context;
        }

        [HttpPost("{orderId}")]
        public async Task<ActionResult<OrderReviewDTO>> CreateReview(int orderId, [FromBody] CreateOrderReviewDTO reviewDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Status != "COMPLETED")
            {
                return BadRequest("只能评价已完成的订单");
            }

            var existingReview = await _context.OrderReviews
                .FirstOrDefaultAsync(r => r.OrderId == orderId);

            if (existingReview != null)
            {
                return BadRequest("该订单已经评价过了");
            }

            var review = new OrderReview
            {
                OrderId = orderId,
                UserId = userId,
                Rating = reviewDTO.Rating,
                Comment = reviewDTO.Comment
            };

            _context.OrderReviews.Add(review);
            await _context.SaveChangesAsync();

            return Ok(new OrderReviewDTO
            {
                Id = review.Id,
                OrderId = review.OrderId,
                UserId = review.UserId,
                UserName = User.FindFirst(ClaimTypes.Name)?.Value,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            });
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<OrderReviewDTO>> GetOrderReview(int orderId)
        {
            var review = await _context.OrderReviews
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.OrderId == orderId);

            if (review == null)
            {
                return NotFound();
            }

            return Ok(new OrderReviewDTO
            {
                Id = review.Id,
                OrderId = review.OrderId,
                UserId = review.UserId,
                UserName = review.User.Username,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            });
        }
    }
} 