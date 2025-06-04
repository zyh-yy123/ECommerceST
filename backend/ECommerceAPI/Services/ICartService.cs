using ECommerceAPI.Models;

namespace ECommerceAPI.Services;

public interface ICartService
{
    Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId);
    Task<CartItem> AddAsync(CartItem item);
    Task<bool> RemoveAsync(int id);
    Task<CartItem?> UpdateQuantityAsync(int id, int quantity);
} 