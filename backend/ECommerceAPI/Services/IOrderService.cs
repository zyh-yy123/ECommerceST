using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
}

