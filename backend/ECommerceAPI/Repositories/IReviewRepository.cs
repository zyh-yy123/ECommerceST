using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetByProductIdAsync(int productId);
        Task AddAsync(Review review);
        Task DeleteAsync(int id);
    }
}

