using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task<Order?> GetByIdAsync(int id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
    }
}


