using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId);
        Task<CartItem?> GetByIdAsync(int id);
        Task AddAsync(CartItem cartItem);
        Task UpdateAsync(CartItem cartItem);
        Task DeleteAsync(int id);
        Task DeleteByUserIdAsync(int userId);
    }
}

