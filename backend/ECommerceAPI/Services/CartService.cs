using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class CartService : ICartService
{
    private readonly ECommerceDbContext _context;

    public CartService(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId)
    {
        return await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<CartItem> AddAsync(CartItem item)
    {
        _context.CartItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item == null) return false;

        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CartItem?> UpdateQuantityAsync(int id, int quantity)
    {
        var item = await _context.CartItems.FindAsync(id);
        if (item == null) return null;

        item.Quantity = quantity;
        await _context.SaveChangesAsync();
        return item;
    }
}

