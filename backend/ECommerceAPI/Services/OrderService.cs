using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class OrderService : IOrderService
{
    private readonly ECommerceDbContext _context;

    public OrderService(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.OrderNumber = GenerateOrderNumber();
        order.CreatedAt = DateTime.UtcNow;
        order.Status = "Pending";

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task<Order?> UpdateStatusAsync(int id, string status)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null) return null;

        order.Status = status;
        await _context.SaveChangesAsync();
        return order;
    }

    private string GenerateOrderNumber()
    {
        return $"ORD{DateTime.UtcNow:yyyyMMddHHmmss}";
    }
}
