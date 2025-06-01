using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public CartRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.CartItems
                .Where(ci => ci.UserId == userId)
                .Include(ci => ci.Product)
                .ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _dbContext.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == id);
        }

        public async Task AddAsync(CartItem cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cartItem = await _dbContext.CartItems.FindAsync(id);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteByUserIdAsync(int userId)
        {
            var items = await _dbContext.CartItems.Where(ci => ci.UserId == userId).ToListAsync();
            _dbContext.CartItems.RemoveRange(items);
            await _dbContext.SaveChangesAsync();
        }
    }
}
