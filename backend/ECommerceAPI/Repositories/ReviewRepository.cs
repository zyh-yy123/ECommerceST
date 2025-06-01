using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ECommerceDbContext _dbContext;

        public ReviewRepository(ECommerceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
        {
            return await _dbContext.Reviews
                .Where(r => r.ProductId == productId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task AddAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review != null)
            {
                _dbContext.Reviews.Remove(review);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
