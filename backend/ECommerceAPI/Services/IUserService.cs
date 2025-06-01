using ECommerceAPI.Models;

namespace ECommerceAPI.Services.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> AddAsync(User user);
    Task<User?> UpdateAsync(int id, User updatedUser);
    Task<bool> DeleteAsync(int id);
}


