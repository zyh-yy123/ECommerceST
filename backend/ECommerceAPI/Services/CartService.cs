using ECommerceAPI.Data;
using ECommerceAPI.Models;
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
        var existingItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.UserId == item.UserId && c.ProductId == item.ProductId);

        if (existingItem != null)
        {
            existingItem.Quantity += item.Quantity;
            await _context.SaveChangesAsync();
            return existingItem;
        }

        _context.CartItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null) return false;

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<CartItem?> UpdateQuantityAsync(int id, int quantity)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null) return null;

        cartItem.Quantity = quantity;
        await _context.SaveChangesAsync();
        return cartItem;
    }
}

