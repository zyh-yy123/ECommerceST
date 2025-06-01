using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task<Product?> UpdateAsync(int id, Product updatedProduct);
    Task<bool> DeleteAsync(int id);
}
