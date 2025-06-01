using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ReviewService : IReviewService
{
    private readonly ECommerceDbContext _context;

    public ReviewService(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetByProductIdAsync(int productId)
    {
        return await _context.Reviews
            .Include(r => r.User)
            .Where(r => r.ProductId == productId)
            .ToListAsync();
    }

    public async Task<Review> AddAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }
}
