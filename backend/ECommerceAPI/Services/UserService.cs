using System.Security.Cryptography;
using System.Text;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;
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

    public async Task<User> RegisterAsync(UserRegistrationDto registrationDto)
    {
        // Check if username or email already exists
        if (await GetUserByUsernameAsync(registrationDto.Username) != null)
        {
            throw new InvalidOperationException("Username already exists");
        }

        if (await GetUserByEmailAsync(registrationDto.Email) != null)
        {
            throw new InvalidOperationException("Email already exists");
        }

        // Create password hash
        var passwordHash = HashPassword(registrationDto.Password);

        var user = new User
        {
            Username = registrationDto.Username,
            PasswordHash = passwordHash,
            Email = registrationDto.Email,
            PhoneNumber = registrationDto.PhoneNumber
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserLoginResponseDto?> LoginAsync(UserLoginDto loginDto)
    {
        var user = await GetUserByUsernameAsync(loginDto.Username);
        if (user == null) return null;

        var hashedPassword = HashPassword(loginDto.Password);
        if (user.PasswordHash != hashedPassword) return null;

        // 生成简单的 token（实际项目中应该使用 JWT）
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        return new UserLoginResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Token = token
        };
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}

