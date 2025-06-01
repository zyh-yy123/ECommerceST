using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public OrderRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _dbContext.Orders.FindAsync(id);
            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
