using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
}

