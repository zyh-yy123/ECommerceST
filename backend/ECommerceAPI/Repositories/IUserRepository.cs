using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerceAPI.Models;

namespace ECommerceAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}

