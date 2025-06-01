using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Services;

public class UserService : IUserService
{
    private readonly ECommerceDbContext _context;

    public UserService(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.Include(u => u.CartItems).Include(u => u.Orders).ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.CartItems)
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(int id, User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Username = updatedUser.Username;
        user.PasswordHash = updatedUser.PasswordHash;
        user.Email = updatedUser.Email;
        user.PhoneNumber = updatedUser.PhoneNumber;

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }
}

