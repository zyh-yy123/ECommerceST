using ECommerceAPI.Models;
using ECommerceAPI.Models.DTOs;

namespace ECommerceAPI.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(int id, User updatedUser);
    Task<bool> DeleteAsync(int id);
    Task<User> RegisterAsync(UserRegistrationDto registrationDto);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByEmailAsync(string email);
    Task<UserLoginResponseDto?> LoginAsync(UserLoginDto loginDto);
}


