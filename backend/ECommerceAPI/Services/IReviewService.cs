using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
    Task<Review> AddAsync(Review review);
}

