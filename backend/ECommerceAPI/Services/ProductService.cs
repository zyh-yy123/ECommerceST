using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class ProductService : IProductService
{
    private readonly ECommerceDbContext _context;

    public ProductService(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.Include(p => p.Reviews).ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Reviews)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> AddAsync(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> UpdateAsync(int id, Product updatedProduct)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return null;

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.Stock = updatedProduct.Stock;
        product.ThumbnailUrl = updatedProduct.ThumbnailUrl;

        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}
